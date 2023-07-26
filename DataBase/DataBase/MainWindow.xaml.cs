using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ADO.Net.Client;

namespace DataBase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString;
        static SqlConnection connection;
        int[] indexes;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void DatabaseConnect()
        {          
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {
                errorNotify.Content = ex.Message;
            }
        }

        public void TableQuery(SqlCommand command, DataGrid dataGrid)
        {
            SqlDataReader dataReader;
            try
            {
                dataReader = command.ExecuteReader();
            }          
            catch (InvalidOperationException e)
            {
                errorNotify.Content = e.Message;
                return;
            }
            catch (InvalidCastException e)
            {
                errorNotify.Content = e.Message;
                return;
            }
            catch (SqlException e)
            {
                errorNotify.Content = e.Message;
                return;
            }
            dataGrid.Columns.Clear();
            if (dataReader.HasRows)
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    DataGridTextColumn column = new DataGridTextColumn
                    {
                        Header = dataReader.GetName(i),
                        Binding = new Binding(string.Format("[{0}]", i))
                    };
                    if (!dataGrid.Columns.Contains(column))
                    {
                        dataGrid.Columns.Add(column);
                    }
                }
                List<object[]> items = new List<object[]>();
                while (dataReader.Read())
                {                   
                    object[] atributes = new object[dataReader.FieldCount];
                    for (int i = 0; i < atributes.Length; i++)
                    {
                        atributes[i] = dataReader.GetValue(i);
                    }
                    items.Add(atributes);
                }
                dataGrid.ItemsSource = items;
            }
            else
            {
                errorNotify.Content = "Запрос обработал 0 строк";
            }
            dataReader.Close();
        }

        public string GetQueryCommand(string columnValue, string valueNull, string valueNotNull)
        {
            string cmd = string.Empty;
            if (columnValue == "NULL" || columnValue == string.Empty || columnValue == "*")
            {
                cmd += valueNull;
            }
            else
            {
                cmd += valueNotNull;
            }
            return cmd;
        }

        public void ClearTextBoxes(TextBox[] boxes)
        {
            for (int i = 0; i < boxes.Length; i++)
            {
                boxes[i].Text = string.Empty;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorNotify();
            string cmdText = string.Empty;
            TextBox textBox = null;
            DataGrid grid = null;
            if ((sender as Button) == sportsmanQueryButton)
            {
                textBox = sportsmanQueryText;
                grid = queryGrid;
                cmdText = "USE Olympiad;\n" +
                          "SELECT titleSport, sportsmanName, nameCountry, titleResult, yearAchievement, nameCoach FROM Sport, Sportsman, Result, Coach, Country\n" +
                          "WHERE resultId = (SELECT resultId FROM Result WHERE resultId = achievement AND resultId < 4) AND\n" +
                          "      coachId = (SELECT coachId FROM Coach WHERE coachId = coachName) AND\n" +
                          "      sportId = (SELECT sport FROM SportsmanSport WHERE sportsman = sportsmanId) AND\n" +
                          "      countryId = (SELECT countryId FROM Country WHERE countryId = countryName)";                
                if (sportsmanCountrySelector.SelectedItem.ToString() != "*")
                {
                    cmdText += $" AND\n      nameCountry = '{sportsmanCountrySelector.SelectedItem}'";
                }
                if (sportsmanSportSelector.SelectedItem.ToString() != "*")
                {
                    cmdText += $" AND\n      titleSport = '{sportsmanSportSelector.SelectedItem}'";
                }
                if (sportsmanYearSelector.SelectedItem.ToString() != "*")
                {
                    cmdText += $" AND\n      yearAchievement = {sportsmanYearSelector.SelectedItem}";
                }
                if (sportsmanResultSelector.SelectedItem.ToString() != "*")
                {
                    cmdText += $" AND\n      titleResult = '{sportsmanResultSelector.SelectedItem}'";
                }
            }
            else if ((sender as Button) == medalQueryButton)
            {
                textBox = medalQueryText;
                grid = queryGrid;
                string year = string.Empty,
                    season = string.Empty;
                if (medalYearSelector.SelectedItem.ToString() != "*")
                {
                    year = medalYearSelector.SelectedItem.ToString();
                }
                if (medalSeasonSelector.SelectedItem.ToString() != "*")
                {
                    if (medalSeasonSelector.SelectedItem.ToString() == "True")
                    {
                        season = "1";
                    }
                    else
                    {
                        season = "0";
                    }
                }

                cmdText = "USE Olympiad;\n" +
                          "SELECT nameCountry, yearAchievement, seasonSummer,\n" +
                         $"(SELECT COUNT(*) FROM Sportsman WHERE (achievement = 1 OR achievement = 2 OR achievement = 3) AND countryId = (SELECT countryId FROM Country WHERE countryId = countryName) AND yearAchievement = {year} AND seasonSummer = (SELECT seasonSummer FROM Sport WHERE sportId = (SELECT sport FROM SportsmanSport WHERE sportsman = sportsmanId))) AS total,\n" +
                         $"(SELECT COUNT(*) FROM Sportsman WHERE achievement = 1 AND countryId = (SELECT countryId FROM Country WHERE countryId = countryName) AND yearAchievement = {year} AND seasonSummer = (SELECT seasonSummer FROM Sport WHERE sportId = (SELECT sport FROM SportsmanSport WHERE sportsman = sportsmanId))) AS gold,\n" +
                         $"(SELECT COUNT(*) FROM Sportsman WHERE achievement = 2 AND countryId = (SELECT countryId FROM Country WHERE countryId = countryName) AND yearAchievement = {year} AND seasonSummer = (SELECT seasonSummer FROM Sport WHERE sportId = (SELECT sport FROM SportsmanSport WHERE sportsman = sportsmanId))) AS silver,\n" +
                         $"(SELECT COUNT(*) FROM Sportsman WHERE achievement = 3 AND countryId = (SELECT countryId FROM Country WHERE countryId = countryName) AND yearAchievement = {year} AND seasonSummer = (SELECT seasonSummer FROM Sport WHERE sportId = (SELECT sport FROM SportsmanSport WHERE sportsman = sportsmanId))) AS bronze\n" +
                          "FROM Sportsman, Country, Sport\n" +
                          "WHERE countryId = (SELECT countryId FROM Country WHERE countryId = countryName) ";
                if (medalCountrySelector.SelectedItem.ToString() != "*")
                {
                    cmdText += $"AND\nnameCountry = '{medalCountrySelector.SelectedItem.ToString()}' ";
                }
                cmdText += $"AND\nyearAchievement = {year} " + 
                           $"AND\nseasonSummer = {season}\n" +
                            "GROUP BY nameCountry, yearAchievement, countryId, seasonSummer";
                if (medalYearSelector.SelectedItem.ToString() == "*" || medalYearSelector.SelectedItem.ToString() == "*")
                {
                    textBox.Text = cmdText;
                    errorNotify.Content = "Ошибка при выполнении запроса! Выберите год участия и/или сезон";
                    return;
                }
            }
            else if ((sender as Button) == tableShowButton)
            {
                grid = showGrid;
                cmdText = $"SELECT * FROM {tableShowSelector.SelectedItem.ToString()}";
            }
            TableQuery(new SqlCommand(cmdText, connection), grid);
            if (textBox != null)
            {
                textBox.Text = cmdText;
            }
        }

        private string GetArgs(string[] columns, string[] values)
        {
            string str = string.Empty;
            for (int i = 0; i < values.Length; i++)
            {
                if (!(values[i] == string.Empty || values[i] == "NULL" || values[i] == "*"))
                {
                    if (i != 0)
                    {
                        str += ", ";
                    }
                    str += $"{columns[i]} = {values[i]}";
                }
            }
            return str;
        }

        private void DDLButton_Click(object sender, RoutedEventArgs e)
        {
            ClearErrorNotify();
            string cmdText = string.Empty;
            TextBox textBox = null;
            TabItem item = null;
            if ((sender as Button) == insertButton)
            {
                item = insertItem;
                if (insertCountry.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertCountry.Header.ToString()}\n" +
                              "(nameCountry)\n" +
                              "VALUES\n" +
                             $"({GetQueryCommand(nameCountryInsertText.Text, "NULL", $"'{nameCountryInsertText.Text}'")})";
                    ClearTextBoxes(new TextBox[] { nameCountryInsertText });
                }
                else if (insertCoach.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertCoach.Header.ToString()}\n" +
                              "(nameCoach)\n" +
                              "VALUES\n" +
                             $"({GetQueryCommand(nameCoachInsertText.Text, "NULL", $"'{nameCoachInsertText.Text}'")})";
                    ClearTextBoxes(new TextBox[] { nameCoachInsertText });
                }
                else if (insertCompetition.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertCompetition.Header.ToString()}\n" +
                              "(nameReferee)\n" +
                              "VALUES\n" +
                              $"({GetQueryCommand(nameRefereeInsertText.Text, "NULL", $"'{nameRefereeInsertText.Text}'")})";
                    ClearTextBoxes(new TextBox[] { nameRefereeInsertText });
                }
                else if (insertCompetitionSportsman.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertCompetitionSportsman.Header.ToString()}\n" +
                              "(competition, participant)\n" +
                              "VALUES\n" +
                             $"({GetQueryCommand(competitionInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{competitionInsertSelector.SelectedItem.ToString().Split(' ')[0]}")}, {GetQueryCommand(participantInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{participantInsertSelector.SelectedItem.ToString().Split(' ')[0]}")})";
                }
                else if (insertSport.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertSport.Header.ToString()}\n" +
                              "(titleSport, seasonSummer)\n" +
                              "VALUES\n" +
                             $"({GetQueryCommand(titleSportInsertText.Text, "NULL", $"'{titleSportInsertText.Text}'")}, {GetQueryCommand(seasonSummerInsertText.Text, "NULL", $"{seasonSummerInsertText.Text}")})";
                    ClearTextBoxes(new TextBox[] { titleSportInsertText, seasonSummerInsertText });
                }
                else if (insertSportsman.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertSportsman.Header.ToString()}\n" +
                              "(sportsmanName, achievement, yearAchievement, countryName, coachName)\n" +
                              "VALUES\n" +
                             $"({GetQueryCommand(sportsmanNameInsertText.Text, "NULL", $"'{sportsmanNameInsertText.Text}'")}, {GetQueryCommand(achievementInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{achievementInsertSelector.SelectedItem.ToString().Split(' ')[0]}")}, {GetQueryCommand(yearAchievementInsertText.Text, "NULL", $"{yearAchievementInsertText.Text}")}, {GetQueryCommand(countryNameInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{countryNameInsertSelector.SelectedItem.ToString().Split(' ')[0]}")}, {GetQueryCommand(coachNameInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{coachNameInsertSelector.SelectedItem.ToString().Split(' ')[0]}")})";
                    ClearTextBoxes(new TextBox[] { sportsmanNameInsertText, yearAchievementInsertText });
                }
                else if (insertSportSportsman.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertSportSportsman.Header.ToString()}\n" +
                              "(sportsman, sport)\n" +
                              "VALUES\n" +
                              $"({GetQueryCommand(sportsmanInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{sportsmanInsertSelector.SelectedItem.ToString().Split(' ')[0]}")}, {GetQueryCommand(sportInsertSelector.SelectedItem.ToString().Split(' ')[0], "NULL", $"{sportInsertSelector.SelectedItem.ToString().Split(' ')[0]}")})";
                }
                else if (insertResult.IsSelected == true)
                {
                    cmdText = "USE Olympiad;\n" +
                             $"INSERT INTO {insertResult.Header.ToString()}\n" +
                              "(titleResult)\n" +
                              "VALUES\n" +
                            $"({GetQueryCommand(titleResultInsertText.Text, "NULL", $"'{titleResultInsertText.Text}'")})";
                    ClearTextBoxes(new TextBox[] { titleResultInsertText });
                }
                textBox = insertQueryText;
            }
            else if ((sender as Button) == deleteButton)
            {
                textBox = deleteQueryText;
                item = deleteItem;
                if (IsSelectedDeleteComboBox(tableDeleteSelector) && IsSelectedDeleteComboBox(columnDeleteSelector) && IsSelectedDeleteComboBox(valueDeleteSelector))
                {
                    cmdText = "USE Olympiad;\n" +
                          $"DELETE FROM {tableDeleteSelector.SelectedItem.ToString()}\n" +
                          $"WHERE {columnDeleteSelector.SelectedItem.ToString()} =";
                    int tempInt;
                    bool tempBool;
                    if (int.TryParse(valueDeleteSelector.SelectedItem.ToString(), out tempInt))
                    {
                        cmdText += $"{valueDeleteSelector.SelectedItem.ToString()} AND ";
                    }
                    else if (bool.TryParse(valueDeleteSelector.SelectedItem.ToString(), out tempBool))
                    {
                        cmdText += $"{valueDeleteSelector.SelectedItem.ToString()} AND ";
                    }
                    else
                    {
                        cmdText += $"'{valueDeleteSelector.SelectedItem.ToString()}' AND ";
                    }
                    cmdText += $"{columnDeleteSelector.Items[1].ToString()} = {valueDeleteSelector.SelectedIndex.ToString()}";
                    textBox = deleteQueryText;
                }
            }
            else if ((sender as Button) == changeButton)
            {
                item = changeItem;
                string table = string.Empty,
                       arguments = string.Empty,
                       expr = string.Empty,
                       expression;
                if (changeCountry.IsSelected == true)
                {
                    table = changeCountry.Header.ToString();
                    arguments = GetArgs(new string[] { "nameCountry" }, new string[] { nameCountryChangeText.Text });
                    expr = $"countryId = ";
                    ClearTextBoxes(new TextBox[] { nameCountryChangeText });
                }
                else if (changeCoach.IsSelected == true)
                {
                    table = changeCoach.Header.ToString();
                    arguments = GetArgs(new string[] { "nameCoach" }, new string[] { nameCoachChangeText.Text });
                    expr = $"coachId = ";
                    ClearTextBoxes(new TextBox[] { nameCoachChangeText });
                }
                else if (changeCompetition.IsSelected == true)
                {
                    table = changeCompetition.Header.ToString();
                    arguments = GetArgs(new string[] { "nameReferee" }, new string[] { nameRefereeChangeText.Text });
                    expr = $"competitionId = ";
                    ClearTextBoxes(new TextBox[] { nameRefereeChangeText });
                }
                else if (changeCompetitionSportsman.IsSelected == true)
                {
                    table = changeCompetitionSportsman.Header.ToString();
                    arguments = GetArgs(new string[] { "competition", "participant" }, new string[] { competitionChangeSelector.SelectedItem.ToString(), participantChangeSelector.SelectedItem.ToString() });
                    expr = $"competitionsportsmanId = ";
                }
                else if (changeSport.IsSelected == true)
                {
                    table = changeSport.Header.ToString();
                    arguments = GetArgs(new string[] { "titleSport", "seasonSummer" }, new string[] { titleSportChangeText.Text, seasonSummerChangeText.Text });
                    expr = $"sportId = ";
                    ClearTextBoxes(new TextBox[] { titleSportChangeText, seasonSummerChangeText });
                }
                else if (changeSportsman.IsSelected == true)
                {
                    table = changeSportsman.Header.ToString();
                    arguments = GetArgs(new string[] { "sportsmaName", "achievement", "yearAchievement", "countryName", "coachName" }, new string[] { sportsmanNameChangeText.Text, achievementChangeSelector.SelectedItem.ToString(), yearAchievementChangeText.Text, countryNameChangeSelector.SelectedItem.ToString(), coachNameChangeSelector.SelectedItem.ToString()});
                    expr = $"sportsmanId = ";
                    ClearTextBoxes(new TextBox[] { sportsmanNameChangeText, yearAchievementChangeText });
                }
                else if (changeSportSportsman.IsSelected == true)
                {
                    table = changeSportSportsman.Header.ToString();
                    arguments = GetArgs(new string[] { "sport", "sportsman" }, new string[] { sportChangeSelector.SelectedItem.ToString(), sportsmanChangeSelector.SelectedItem.ToString() });
                    expr = $"sportsportsmanId = ";
                }
                else if (changeResult.IsSelected == true)
                {
                    table = changeResult.Header.ToString();
                    arguments = GetArgs(new string[] { "titleResult" }, new string[] { titleResultChangeText.Text });
                    expr = $"resultId = ";
                    ClearTextBoxes(new TextBox[] { titleResultChangeText });
                }
                expression = expr + idChangeSelector.SelectedItem.ToString();
                cmdText = "USE Olympiad;\n" +
                         $"UPDATE {table}\n" +
                         $"SET {arguments}\n" +
                         $"WHERE {expression}\n";
                textBox = changeQueryText;
            }
            SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
            try
            {
                sqlCommand.ExecuteNonQuery();
                GetItemsForSelectors();
                TabControl_SelectionChanged(item, e as SelectionChangedEventArgs);
            }
            catch (InvalidOperationException ex)
            {
                errorNotify.Content = ex.Message;
                return;
            }
            catch (InvalidCastException ex)
            {
                errorNotify.Content = ex.Message;
                return;
            }
            catch (SqlException ex)
            {
                errorNotify.Content = ex.Message;
                return;
            }
            textBox.Text = cmdText;
        }

        private void GetItemsForSelectors()
        {
            ClearComboBoxes(new ComboBox[] { sportsmanCountrySelector, medalCountrySelector, sportsmanYearSelector, medalYearSelector, sportsmanResultSelector, sportsmanSportSelector, medalSeasonSelector, tableShowSelector, tableDeleteSelector, achievementInsertSelector, coachNameInsertSelector, countryNameInsertSelector, competitionInsertSelector, participantInsertSelector, sportInsertSelector, sportsmanInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT nameCountry FROM Country", connection), new ComboBox[] { sportsmanCountrySelector, medalCountrySelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT DISTINCT yearAchievement FROM Sportsman", connection), new ComboBox[] { sportsmanYearSelector, medalYearSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT titleSport FROM Sport", connection), new ComboBox[] { sportsmanSportSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT titleResult FROM Result", connection), new ComboBox[] { sportsmanResultSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT DISTINCT seasonSummer FROM Sport", connection), new ComboBox[] { medalSeasonSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT name FROM sys.tables", connection), new ComboBox[] { tableShowSelector, tableDeleteSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT sportsmanId, sportsmanName FROM Sportsman", connection), new ComboBox[] { participantInsertSelector, sportsmanInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT resultId, titleResult FROM Result", connection), new ComboBox[] { achievementInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT coachId, nameCoach FROM Coach", connection), new ComboBox[] { coachNameInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT competitionId, nameReferee FROM Competition", connection), new ComboBox[] { competitionInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT countryId, nameCountry FROM Country", connection), new ComboBox[] { countryNameInsertSelector });
            GetItemsForComboBoxes(new SqlCommand("SELECT sportId, titleSport FROM Sport", connection), new ComboBox[] { sportInsertSelector });
        }

        private void ClearComboBoxes(ComboBox[] comboBoxes)
        {
            for (int i = 0; i< comboBoxes.Length; i++)
            {
                comboBoxes[i].ItemsSource = new List<string>();
            }
        }

        private void GetItemsForComboBoxes(SqlCommand sqlCommand, ComboBox[] selectors)
        {
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            List<object> items = new List<object>();
            items.Add("*");
            while (dataReader.Read())
            {
                string row = string.Empty;
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (i != 0)
                    {
                        row += " ";
                    }
                    row += dataReader.GetValue(i);
                }
                items.Add(row);
            }
            for (int i = 0; i < selectors.Length; i++)
            {
                selectors[i].ItemsSource = items;
                selectors[i].SelectedItem = selectors[i].Items[0];
            }
            dataReader.Close();
        }

        private void ClearErrorNotify() => errorNotify.Content = string.Empty;

        private void tableDeleteSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearErrorNotify();
            if ((sender as ComboBox) == tableDeleteSelector)
            {
                ClearComboBoxes(new ComboBox[] { columnDeleteSelector });
                if (IsSelectedDeleteComboBox(sender as ComboBox))
                {
                    ClearComboBoxes(new ComboBox[] { valueDeleteSelector });
                    GetItemsForComboBoxes(new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{(sender as ComboBox).SelectedItem.ToString()}'", connection), new ComboBox[] { columnDeleteSelector });
                    TableQuery(new SqlCommand($"SELECT * FROM {(sender as ComboBox).SelectedItem.ToString()} ORDER BY {columnDeleteSelector.Items[1]}", connection), deleteGrid);
                }
                else
                {
                    deleteGrid.Columns.Clear();
                    deleteGrid.ItemsSource = new List<object[]>();
                }
            }
            else if ((sender as ComboBox) == columnDeleteSelector)
            {
                ClearComboBoxes(new ComboBox[] { valueDeleteSelector });
                if (IsSelectedDeleteComboBox((sender as ComboBox)))
                {
                    GetItemsForComboBoxes(new SqlCommand($"SELECT {(sender as ComboBox).SelectedItem.ToString()} FROM {tableDeleteSelector.SelectedItem.ToString()} ORDER BY {columnDeleteSelector.Items[1]}", connection), new ComboBox[] { valueDeleteSelector });
                    indexes = GetIndexes(tableDeleteSelector.SelectedItem.ToString(), columnDeleteSelector.Items[1].ToString());
                }
            }
        }

        private int[] GetIndexes(string table, string column)
        {
            SqlCommand sqlCommand = new SqlCommand($"SELECT {column} FROM {table}", connection);
            SqlDataReader dataReader = sqlCommand.ExecuteReader();
            List<int> list = new List<int>();
            if (dataReader.HasRows)
            {
                while(dataReader.Read())
                {
                    list.Add((int)dataReader.GetValue(0));
                }
            }
            int[] arr = list.ToArray();
            dataReader.Close();
            return arr;
        }

        private bool IsSelectedDeleteComboBox(ComboBox comboBox)
        {
            return comboBox.SelectedItem != null && comboBox.SelectedItem.ToString() != "*" && comboBox.Items.Count != 0;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearErrorNotify();
            if (insertItem.IsSelected == true)
            {
                TabItem item = null;
                if (insertCoach.IsSelected == true)
                {
                    item = insertCoach;
                }
                else if (insertCountry.IsSelected == true)
                {
                    item = insertCountry;
                }
                else if (insertCompetition.IsSelected == true)
                {
                    item = insertCompetition;
                }
                else if (insertCompetitionSportsman.IsSelected == true)
                {
                    item = insertCompetitionSportsman;
                }
                else if (insertSport.IsSelected == true)
                {
                    item = insertSport;
                }
                else if (insertSportsman.IsSelected == true)
                {
                    item = insertSportsman;
                }
                else if (insertSportSportsman.IsSelected == true)
                {
                    item = insertSportSportsman;
                }
                else if (insertResult.IsSelected == true)
                {
                    item = insertResult;
                }
                if (item != null)
                {
                    TableQuery(new SqlCommand($"SELECT * FROM {item.Header.ToString()}", connection), insertGrid);
                }
            }
            else if (deleteItem.IsSelected == true)
            {
                if (IsSelectedDeleteComboBox(tableDeleteSelector))
                {
                    TableQuery(new SqlCommand($"SELECT * FROM {tableDeleteSelector.SelectedItem.ToString()} ORDER BY {tableDeleteSelector.SelectedItem.ToString().ToLower()}Id", connection), deleteGrid);
                }
                else
                {
                    deleteGrid.Columns.Clear();
                    deleteGrid.ItemsSource = new List<object[]>();
                }
            }
            else if (changeItem.IsSelected == true)
            {
                TabItem item = null;
                if (changeCoach.IsSelected == true)
                {
                    item = changeCoach;
                }
                else if (changeCountry.IsSelected == true)
                {
                    item = changeCountry;
                }
                else if (changeCompetition.IsSelected == true)
                {
                    item = changeCompetition;
                }
                else if (changeCompetitionSportsman.IsSelected == true)
                {
                    item = changeCompetitionSportsman;
                }
                else if (changeSport.IsSelected == true)
                {
                    item = changeSport;
                }
                else if (changeSportsman.IsSelected == true)
                {
                    item = changeSportsman;
                }
                else if (changeSportSportsman.IsSelected == true)
                {
                    item = changeSportSportsman;
                }
                else if (changeResult.IsSelected == true)
                {
                    item = changeResult;
                }
                if (item != null)
                {
                    TableQuery(new SqlCommand($"SELECT * FROM {item.Header.ToString()}", connection), changeGrid);
                    GetItemsForComboBoxes(new SqlCommand($"SELECT {item.Header.ToString().ToLower()}Id FROM {item.Header.ToString()}", connection), new ComboBox[] { idChangeSelector });
                }
            }
        }

        private void SetEnablePropertyForTabItems(bool propertyValue, TabItem[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].IsEnabled = propertyValue;
            }
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            if (connectButton.Content.ToString() == "Connect")
            {
                connectionString = $@"Data Source=.\SQLEXPRESS;Initial Catalog=Olympiad;User ID={loginText.Text};Password={passwordText.Text};";
                connection = new SqlConnection(connectionString);
                try
                {
                    connection.Open();
                    connectButton.Content = "Disconnect";
                    GetItemsForSelectors();
                    SetEnablePropertyForTabItems(true, new TabItem[] { insertItem, deleteItem, changeItem, queryItem, tableItem });
                    userLogin.Content = $"login: {loginText.Text}";
                }
                catch (SqlException ex)
                {
                    errorNotify.Content = ex.Message;
                }
            }
            else
            {
                connection.Close();
                connectButton.Content = "Connect";
                SetEnablePropertyForTabItems(false, new TabItem[] { insertItem, deleteItem, changeItem, queryItem, tableItem });
                userLogin.Content = "login:";
            }
        }
    }
}
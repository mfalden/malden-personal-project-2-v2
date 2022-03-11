using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


    public class HighScoreTracker
    {

        /// <summary>
        /// Runs the program code.
        /// </summary>
        public static void RunCode()
        {
            FancyConsole.Clear();
            FancyConsole.Refresh();
            string fileName = "scoresFile.txt";
            List<string> scoreList = LoadScoresFile(fileName);
            List<int> scoresOnly = ScoreSplit(scoreList);
            (int userScore, string userName) = UserScore();
            int insertAt = ScoreCompare(scoresOnly, userScore);
            FancyConsole.Clear();
            FancyConsole.Refresh();
            AddScore(userName, userScore, insertAt, scoreList, fileName);
        }
        /// <summary>
        /// Loads the "scoresfile.txt" file and stores it in list "ScoreList". 
        /// </summary>
        /// <param name="scoresFile">a .txt file storing all scores.</param>
        /// <returns>Returns "ScoreList".</returns>
        public static List<string> LoadScoresFile(string scoresFile)
        {
            if (scoresFile == null)
            {
                throw new Exception("String scoresFile is null!");
            }
            // 1. Create list scoreList
            // 2. scorelist = file.ReadLines(scoresfile.txt);
            // 3. return list scorelist
            List<string> scoreList;
            if (File.Exists(scoresFile) == false)
            {
                throw new Exception($"The file {scoresFile} does not exist.");
            }
            scoreList = File.ReadAllLines(scoresFile).ToList();

            return scoreList;
        }

        /// <summary>
        /// Loads the list "ScoreList" and takes the score values only.
        /// </summary>
        /// <param name="scoreList">A list of all usernames and their scores</param>
        /// <returns>Returns int list "ScoresOnly".</returns>
        public static List<int> ScoreSplit(List<string> scoreList)
        {

            if (scoreList == null)
            {
                throw new Exception("String scoreList is null!");
            }


            // 1. Split the scoreList along all spaces (" ")
            // 2. create new list<int> = scoresOnly
            // 3. add element 2 using int.Parse(scoresOnly[1]);
            // 4. return list<int> scoresOnly;

            List<int> scoresOnly = new List<int>();
            foreach (string line in scoreList)
            {
                if (!line.Equals(""))
                {
                    scoresOnly.Add(int.Parse(line.Split(' ')[1]));
                }
            }
            return scoresOnly;
        }

        /// <summary>
        /// prompts the user's name and score values and stores them in two strings, "userScore" and "userName".
        /// </summary>
        /// <returns>The function returns userScore and userName.</returns>
        public static (int, string) UserScore()
        {
            // 1. create string userName
            // 2. create int userScore
            // 3. display "please type in your name"
            // 4. collect user input
            // 5. trim user input
            // 6. add to string userName
            // 7. start of loop: display "please type in your score"
            // 8. collect user input
            // 9. trim user input
            // 10. if the score includes letters, display "invalid score" and restart the loop. If the score is only numbers, add the user input to integer userScore
            // 11. Return int userScore, string userName
            FancyConsole.Write(6, 10, "Please type in your name: ");
            string userName = Console.ReadLine().Replace(" ", "");
            int score = Program.spaces;
            return (score, userName);
        }

        /// <summary>
        /// Takes the string "userscore" and compares it to the values in scoresOnly, stopping only when the userScore is greater than the value in an index of scoresOnly.
        /// </summary>
        /// <param name="scoresOnly">A list containing the scores of past players</param>
        /// <param name="userScore">The user's score</param>
        /// <returns>returns the index number of the row where userScore was greater than scoresOnly in an integer "insertAt".</returns>
        public static int ScoreCompare(List<int> scoresOnly, int userScore)
        {
            // 1. load list<int> scoresOnly and int userScore
            // 2. create new int inserAt and set to 0
            // 3. start of loop: for each line in scoresOnly--
            // 4. if the user score is less than scoresOnly, increase int insertAt by one and restart the loop
            // 5.0 if the user score is equal to scoresOnly, return int insertAt
            // 5. if the user score is greater than scores only, return int insertAt
            if (scoresOnly == null)
            {
                throw new Exception("String scoresOnly is null!");
            }

            int insertAt = 0;
            foreach (int line in scoresOnly)
            {
                if (userScore > line)
                {
                    insertAt = insertAt + 1;
                }
                else
                {
                    return insertAt;
                }
            }
            return insertAt;


        }

        /// <summary>
        /// Takes the "userName" and "userScore" strings, combines them into a string entry and inserts them into the list "scorelist" at the index specified by int "insertAt". It then displays all scores.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userScore"></param>
        /// <param name="insertAt"></param>
        /// <param name="scoreList"></param>
        public static List<string> AddScore(string userName, int userScore, int insertAt, List<string> scoreList, string fileName)
        {
            // string entry;
            // 1. Load in the userName, userScore, insertAt, and scoreList variables. 
            // 2. Create String "entry" $"{userName} {userScore}"
            // 3. Insert "entry" at index "insertAt" 
            // 4. Using WriteLine, Display list scoreList
            // 5. Using File.WriteLines, override all entries in scoresFile.txt to be entries from list scoreList
            if (userName == null)
            {
                throw new Exception("String userName is null!");
            }
            if (scoreList == null)
            {
                throw new Exception("List scoreList is null!");
            }
            if (fileName == null)
            {
                throw new Exception("String fileName is null!");
            }
            string entry;
            string scoreString = userScore.ToString();
            entry = $"{userName} {userScore}";
            scoreList.Insert(insertAt, entry);
            int row = 14;
            foreach (string line in scoreList)
            {
                FancyConsole.Write(row, 10, $"{line}");
                row++;
            }
            File.WriteAllLines(fileName, scoreList);
            return scoreList;

        }
    }


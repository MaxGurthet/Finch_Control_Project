using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    // Title: Finch Control - Menu Starter
    // Description: FIX THIS!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // Application Type: Console
    // Author: Gurthet, Max
    // Dated Created: 2/22/2021
    // Last Modified: 2/22/2021
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method runs when app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":

                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }


        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) ");
                Console.WriteLine("\tc) ");
                Console.WriteLine("\td) ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case "b":

                        break;

                    case "c":

                        break;

                    case "d":

                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region DATA RECORDER
        /// <summary>
        /// *****************************************************************
        /// *                     Data Recorder                             *
        /// *****************************************************************
        /// </summary>
        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;

            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\tq) Back to Main Menu");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();
                
                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }

        /// <summary>
        /// Display data as a table to the user
        /// </summary>
        static void DataRecorderDisplayGetData(double[] temperatures)
        {
            DisplayScreenHeader("Show Data");

            DataRecorderDisplayTable(temperatures);

            DisplayContinuePrompt();
        }
        
        /// <summary>
        /// Get the table structure
        /// </summary>
        static void DataRecorderDisplayTable(double[] temperatures)
        {
            //
            //display table headers to the user
            //
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp".PadLeft(15)
                );
            Console.WriteLine(
                "***********".PadLeft(15) +
                "***********".PadLeft(15)
                );

            //
            //display table data to user
            //
            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                    (index + 1).ToString().PadLeft(15) +
                    temperatures[index].ToString().PadLeft(15)
                    );
            }
        }

        /// <summary>
        /// Get data points
        /// </summary>
        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Data");
            Console.WriteLine($"\tNumber of data points: {numberOfDataPoints}");
            Console.WriteLine($"\tData point frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("\tThe finch robot is ready to begin recording temperature data.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                Console.WriteLine($"\tReading {index + 1}: {temperatures[index]}");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }

            DisplayContinuePrompt();
            return temperatures;
        }

        /// <summary>
        /// Get frequency of data points from the user
        /// </summary>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            string userResponse;
            bool validResponse;

            //
            //get and validate user input
            //
            do
            {
                DisplayScreenHeader("Data Point Frequency");

                Console.Write("Please enter the frequency of the data points in seconds: ");
                userResponse = Console.ReadLine();

                if (!double.TryParse(userResponse, out dataPointFrequency))
                {
                    validResponse = false;
                    Console.WriteLine("It appears you have entered an invalid input. Please enter a valid number.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            DisplayContinuePrompt();

            return dataPointFrequency;
        }

        /// <summary>
        /// get number of data points from the user
        /// </summary>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string userResponse;
            bool validResponse;

            //
            //get and validate user input
            //
            do
            {
                DisplayScreenHeader("Number of Data Points");

                Console.Write("Please enter the number of data points: ");
                userResponse = Console.ReadLine();

                if (!int.TryParse(userResponse, out numberOfDataPoints))
                {
                    validResponse = false;
                    Console.WriteLine("It appears you have entered an invalid input. Please enter a valid number.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            DisplayContinuePrompt();
            
            return numberOfDataPoints;
        }

        #endregion

        #region ALARM SYSTEM
        /// <summary>
        /// *****************************************************************
        /// *                     Alarm System                              *
        /// *****************************************************************
        /// </summary>
        static void LightAlarmDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMontior = 0;

            do
            {
                DisplayScreenHeader("Light Alarm System Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te) Set Alarm");
                Console.WriteLine("\tq) Back to Main Menu");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;
                        
                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmSetMinMaxThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMontior = LightAlarmSetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMontior);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }

        /// <summary>
        /// Prompt the user to set the alarm
        /// </summary>
        static void LightAlarmSetAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMontior)
        {
            int secondsElapsed = 0;
            bool thresholdExceeded = false;
            int currentLightSensorValue = 0;

            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"The sensor(s) being used are {sensorsToMonitor}");
            Console.WriteLine("The range type is {0} ", rangeType);
            Console.WriteLine("The min/max threshold value is " + minMaxThresholdValue);
            Console.WriteLine($"Time set to monitor is {timeToMontior}");
            Console.WriteLine();

            Console.WriteLine("Press and key to begin the monitoring process.");
            Console.ReadKey();
            Console.WriteLine();

            while ((secondsElapsed < timeToMontior) && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "Left":
                        currentLightSensorValue = finchRobot.getLeftLightSensor();
                        break;

                    case "Right":
                        currentLightSensorValue = finchRobot.getRightLightSensor();
                        break;

                    case "Both":
                        currentLightSensorValue = (finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2;
                        break;
                }

                switch (rangeType)
                {
                    case "Minimum":
                        if(currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "Maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                finchRobot.wait(1000);
                secondsElapsed++;
            }

            if (thresholdExceeded)
            {
                Console.WriteLine($"\tThe {rangeType} threshold value of {minMaxThresholdValue} was exceeded by the current light sensor value of {currentLightSensorValue}.");
            }
            else
            {
                Console.WriteLine($"\tThe {rangeType} threshold value of {minMaxThresholdValue} was not exceeded.");
            }
            DisplayMenuPrompt("Light Alarm System Menu");
        }

        /// <summary>
        /// Prompt the user to input the time to montior
        /// </summary>
        static int LightAlarmSetTimeToMonitor()
        {
            int timeToMonitor;
            bool validResponse;

            //prompt user for input and validate response
            do
            {
                DisplayScreenHeader("Time to Monitor");

                Console.Write($"\tTime to monitor >> ");

                if (!int.TryParse(Console.ReadLine(), out timeToMonitor))
                {
                    validResponse = false;
                    Console.WriteLine("It appears you have entered an invalid input. Please enter a valid number.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);

            DisplayMenuPrompt("Light Alarm System Menu");

            return timeToMonitor;
        }

        /// <summary>
        /// Get the minimum/maximum threshold value from the user
        /// </summary>
        static int LightAlarmSetMinMaxThresholdValue(string rangeType, Finch finchRobot)
        {
            int minMaxThresholdValue;
            bool validResponse;

            //prompt user for input and validate response
            do
            {
                DisplayScreenHeader("Minimum/Maximum Threshold Value");

                Console.WriteLine($"\tThe left light sensor ambient value is {finchRobot.getLeftLightSensor()}");
                Console.WriteLine($"\tThe right light sensor ambient value is {finchRobot.getRightLightSensor()}");
                Console.WriteLine();

                Console.Write($"\tPlease enter the {rangeType} light sensor value >> ");

                if (!int.TryParse(Console.ReadLine(), out minMaxThresholdValue))
                {
                    validResponse = false;
                    Console.WriteLine("It appears you have entered an invalid input. Please enter a valid number.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }
                else
                {
                    validResponse = true;
                }
            } while (!validResponse);

            DisplayMenuPrompt("Light Alarm System Menu");

            return minMaxThresholdValue;
        }

        /// <summary>
        /// Prompt the user to select which sensor(s) to use
        /// </summary>
        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor;
            bool validResponse;

            //promt user for input and validate input
            //
            do
            {
                DisplayScreenHeader("Sensor to Monitor");

                validResponse = true;
                
                Console.WriteLine("Please enter what sensors you want to use.");
                Console.WriteLine("The options are as follows: [Left, Right, Both]");
                Console.Write("Please enter your selection >> ");
                sensorsToMonitor = Console.ReadLine();

                if (sensorsToMonitor != "Left" && sensorsToMonitor != "Right" && sensorsToMonitor != "Both")
                {
                    validResponse = false;
                    Console.WriteLine();
                    Console.WriteLine("It appears you have entered an option that is not available/invalid. Please reenter your selection.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }

            } while (!validResponse);

            DisplayMenuPrompt("Light Alarm System Menu");

            return sensorsToMonitor;
        }

        /// <summary>
        /// Prompt the user to select the range type
        /// </summary>
        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;
            bool validResponse;

            //prompt user for input and validate
            //
            do
            {
                DisplayScreenHeader("Range Type");

                validResponse = true;

                Console.WriteLine("Please enter what sensors you want to use.");
                Console.WriteLine("The options are as follows: [Minimum, Maximum]");
                Console.Write("Please enter your selection >> ");
                rangeType = Console.ReadLine();

                if (rangeType != "Minimum" && rangeType != "Maximum")
                {
                    validResponse = false;
                    Console.WriteLine();
                    Console.WriteLine("It appears you have entered an option that is not available/invalid. Please reenter your selection.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    Console.WriteLine();
                    Console.Clear();
                }

            } while (!validResponse);

            DisplayMenuPrompt("Light Alarm System Menu");

            return rangeType;
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tYou are about to disconnect from your Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tYour robot is now disconnected.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tYou are attempting to connect to your Finch robot.");
            Console.WriteLine("\tPlease make sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            //
            //connect robot
            //
            if (robotConnected)
            {
                Console.WriteLine("\tConnection Confirmed.");
                finchRobot.setLED(0, 255, 0);
                finchRobot.noteOn(300);
                finchRobot.wait(1000);
            }
            else
            {
                Console.WriteLine("\tFinch is not connected, please check your connection and try again.");
                DisplayContinuePrompt();
            }

            //
            //reset robot and return to main menu
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();
            DisplayMenuPrompt("Main Menu");

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\tWelcome to the Finch Control Program!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Finch Control Program!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName}.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}

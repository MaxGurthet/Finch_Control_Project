﻿using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    // Title: Finch Control - Menu Starter
    // Description: A program that will allow the user
    //              to take control of their Finch robot
    //              through various options
    // Application Type: Console
    // Author: Gurthet, Max
    // Dated Created: 2/22/2021
    // Last Modified: 3/9/2021
    // **************************************************

    #region USER COMMANDS
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        DONE
    }
    #endregion
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
            DisplayLoginRegisterMenu();
            ThemeMenuOperation();
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
        /// main menu
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
                        UserProgrammingDisplayMenuScreen(finchRobot);
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

        #region USER PROGRAMMING
        static void UserProgrammingDisplayMenuScreen(Finch finchRobot)
        {
            bool quitMenu = false;
            string menuChoice;

            //
            //store command parameters
            //
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        UserProgrammingDisplayFinchCommands(commands);
                        break;

                    case "d":
                        UserProgrammingDisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
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
        /// execute the user's commands
        /// </summary>
        /// <param name="finchRobot"></param>
        /// <param name="commands"></param>
        /// <param name="commandParameters"></param>
        static void UserProgrammingDisplayExecuteFinchCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds = 1000);
            string commandFeedback = "";
            const int TURNING_MOTOR_SPEED = 100;

            DisplayScreenHeader("Execute Finch Commands");

            Console.WriteLine("\tThe Finch robot is now prepared to execute the list of commands.");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandFeedback = Command.MOVEFORWARD.ToString();
                        ; break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandFeedback = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandFeedback = Command.WAIT.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandFeedback = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(TURNING_MOTOR_SPEED, -TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-TURNING_MOTOR_SPEED, TURNING_MOTOR_SPEED);
                        commandFeedback = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        commandFeedback = Command.LEDON.ToString();
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        commandFeedback = Command.LEDOFF.ToString();
                        break;

                    case Command.GETTEMPERATURE:
                        commandFeedback = $"Temperature: {finchRobot.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.DONE:
                        commandFeedback = Command.DONE.ToString();
                        break;

                    default:

                        break;
                }

                Console.WriteLine($"\t{commandFeedback}");
            }

            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        /// display the user's entered commands
        /// </summary>
        /// <param name="commands"></param>
        static void UserProgrammingDisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine($"\t{command}");
            }

            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        /// prompt the user to input commands
        /// </summary>
        /// <param name="commands"></param>
        static void UserProgrammingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            //
            //display list of commands
            //
            int commandCount = 1;
            Console.WriteLine("\tList of available commands are as follows:");
            Console.WriteLine();
            Console.Write("\t-");
            foreach (string commandName in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"- {commandName.ToLower()} -");
                if (commandCount % 5 == 0) Console.Write("-\n\t-");
                commandCount++;
            }
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.Write("\tEnter command: ");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t*******************************************");
                    Console.WriteLine("\t\tPlease enter a command from the list above.");
                    Console.WriteLine("\t\t*******************************************");
                }
            }

            DisplayMenuPrompt("User Programming");
        }

        /// <summary>
        /// get command parameters
        /// </summary>
        /// <returns></returns>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            DisplayScreenHeader("Command Parameters");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            GetValidInteger("\tEnter motor speed [1 - 255]: ", 1, 255, out commandParameters.motorSpeed);
            GetValidInteger("\tEnter LED brightness [1 - 255]: ", 1, 255, out commandParameters.ledBrightness);
            GetValidDouble("\tEnter wait in seconds: [0 - 10]: ", 0, 10, out commandParameters.waitSeconds);

            Console.WriteLine();
            Console.WriteLine($"\tMotor speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"\tLED brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"\tWait command duration: {commandParameters.waitSeconds}");

            DisplayMenuPrompt("User Programming");

            return commandParameters;
        }

        /// <summary>
        /// validate waitSeconds input
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="waitSeconds"></param>
        static void GetValidDouble(string v1, int v2, int v3, out double waitSeconds)
        {
            bool validAnswer = false;
            do
            {
                Console.Write(v1);
                bool yesDouble = Double.TryParse(Console.ReadLine(), out waitSeconds);

                if (yesDouble == false || waitSeconds < 0 || waitSeconds > 10)

                {
                    Console.WriteLine("Your input is invalid, please enter an number between '0' and '10'");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
                else
                {
                    validAnswer = true;
                }
            } while (!validAnswer);
        }

        /// <summary>
        /// validate motorSpeed and ledBrightness inputs
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="v3"></param>
        /// <param name="motorSpeed"></param>
        static void GetValidInteger(string v1, int v2, int v3, out int motorSpeed)
        {
            bool validAnswer = false;
            do
            {
                Console.Write(v1);
                bool isNumber = Int32.TryParse(Console.ReadLine(), out motorSpeed);

                if (isNumber == true && motorSpeed <= 255 && motorSpeed >= 1)
                {
                    validAnswer = true;
                }
                else
                {
                    Console.WriteLine("Your input is invalid, please enter an number between '1' and '255'");
                    DisplayContinuePrompt();
                    Console.Clear();
                }
            } while (!validAnswer);
        }

        #endregion

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mix It Up");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice: ");
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
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case "c":
                        TalentShowDisplayMixingItUp(finchRobot);
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
        /// make the robot sing a song
        /// </summary>
        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            DisplayScreenHeader("Mix It Up");

            Console.WriteLine("\tThe robot will now perform Hot Cross Buns!");
            DisplayContinuePrompt();

            finchRobot.noteOn(494);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(1000);
            finchRobot.noteOn(494);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(1000);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(392);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(440);
            finchRobot.wait(250);
            finchRobot.noteOn(494);
            finchRobot.wait(500);
            finchRobot.noteOn(440);
            finchRobot.wait(500);
            finchRobot.noteOn(392);
            finchRobot.wait(1000);
            finchRobot.noteOff();

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// make the robot peform a short dance
        /// </summary>
        static void TalentShowDisplayDance(Finch finchRobot)
        {
            DisplayScreenHeader("Dance");

            Console.WriteLine("\tThe Robot will now start it's dance!");
            DisplayContinuePrompt();

            finchRobot.setLED(255, 0, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(180, -180);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 255, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(-180, 180);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 0, 255);
            finchRobot.wait(100);
            finchRobot.setMotors(180, 180);
            finchRobot.wait(500);
            finchRobot.setMotors(-180, -180);
            finchRobot.wait(500);
            finchRobot.setLED(255, 255, 0);
            finchRobot.wait(100);
            finchRobot.setMotors(-180, 180);
            finchRobot.wait(1000);
            finchRobot.setLED(0, 255, 255);
            finchRobot.wait(100);
            finchRobot.setMotors(180, -180);
            finchRobot.wait(1000);
            finchRobot.setLED(255, 0, 255);
            finchRobot.wait(100);
            finchRobot.setMotors(180, 180);
            finchRobot.wait(500);
            finchRobot.setMotors(-180, -180);
            finchRobot.wait(500);
            finchRobot.setMotors(0, 0);
            finchRobot.setLED(0, 0, 0);

            DisplayMenuPrompt("Talent Show Menu");
        }

        /// <summary>
        /// make the robot perform a long note and glow
        /// </summary>
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
        /// *                     Data Recorder Menu                        *
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
                        if (currentLightSensorValue < minMaxThresholdValue)
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
        /// welcome screen
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
        /// closing screen
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

        #region LOGIN/REGISTER INTERFACE

        /// <summary>
        /// dispay the login/register menu
        /// </summary>
        static void DisplayLoginRegisterMenu()
        {
            DisplayScreenHeader("Login/Register");

            Console.Write("\tAre you currently a registered user, yes or no?: ");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLoginScreen();
                Console.WriteLine("\tYou will now be redirected to the theme select screen.");
                DisplayContinuePrompt();
            }
            else
            {
                DisplayRegisterScreen();
                DisplayLoginScreen();
                Console.WriteLine("\tYou will now be redirected to the theme select screen.");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// user login screen
        /// </summary>
        static void DisplayLoginScreen()
        {
            string userName;
            string password;
            bool validAns;

            do
            {
                DisplayScreenHeader("User Login");

                Console.WriteLine();
                Console.Write("\tPlease enter your username: ");
                userName = Console.ReadLine();
                Console.Write("\tPlease enter your password: ");
                password = Console.ReadLine();

                validAns = ValidLogin(userName, password);

                Console.WriteLine();
                if (validAns)
                {
                    Console.WriteLine("\tYou have successfully logged in.");
                }
                else
                {
                    Console.WriteLine("\tLogin failed, please check username and password and try again.");
                }
            } while (!validAns);
        }

        /// <summary>
        /// validates the user's login information
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool ValidLogin(string userName, string password)
        {
            (string userName, string password) userInfo;
            bool validAns;

            userInfo = ReadLoginInfo();

            validAns = (userInfo.userName == userName) && (userInfo.password == password);

            return validAns;
        }

        /// <summary>
        /// reads the user's login info from a data file
        /// </summary>
        /// <returns></returns>
        static (string userName, string password) ReadLoginInfo()
        {
            string dataPath = @"Data/Logins.txt";

            string loginInfoText;
            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            loginInfoText = File.ReadAllText(dataPath);
            loginInfoArray = loginInfoText.Split(',');
            loginInfoTuple.userName = loginInfoArray[0];
            loginInfoTuple.password = loginInfoArray[1];

            return loginInfoTuple;
        }

        /// <summary>
        /// writes login info to a data file
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        static void WriteLoginInfo(string userName, string password)
        {
            string dataPath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password;

            File.WriteAllText(dataPath, loginInfoText);
        }

        /// <summary>
        /// user register screen
        /// </summary>
        static void DisplayRegisterScreen()
        {
            string userName;
            string password;

            DisplayScreenHeader("Register User");

            Console.Write("\tPlease enter your desired username: ");
            userName = Console.ReadLine();
            Console.Write("\tPlease enter your desired password: ");
            password = Console.ReadLine();

            WriteLoginInfo(userName, password);

            Console.WriteLine();
            Console.WriteLine("\tThe following information you have entered will be saved.");
            Console.WriteLine($"\tUsername: {userName}");
            Console.WriteLine($"\tPassword: {password}");

            DisplayContinuePrompt();
        }

        #endregion

        #region THEME SELECT INTERFACE
        
        /// <summary>
        /// the code entered in the main for the theme color menu
        /// </summary>
        static void ThemeMenuOperation()
        {
            DisplayReadAndSetTheme();
            DisplaySetNewTheme();
        }
        /// <summary>
        /// read and set the theme screen
        /// </summary>
        static void DisplayReadAndSetTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            string fileIOStatusMessage;

            //
            // read theme from data and set theme
            //
            themeColors = ReadThemeDataExceptions(out fileIOStatusMessage);
            if (fileIOStatusMessage == "Complete")
            {
                Console.ForegroundColor = themeColors.foregroundColor;
                Console.BackgroundColor = themeColors.backgroundColor;
                Console.Clear();

                DisplayScreenHeader("Read Theme from Data File");
                Console.WriteLine("\n\tTheme read from data file.\n");
            }
            else
            {
                DisplayScreenHeader("Read Theme from Data File");
                Console.WriteLine("\n\tTheme not read from data file.");
                Console.WriteLine($"\t*** {fileIOStatusMessage} ***\n");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// prompts the user to either select new theme or continue
        /// </summary>
        static void DisplaySetNewTheme()
        {
            (ConsoleColor foregroundColor, ConsoleColor backgroundColor) themeColors;
            bool themeChosen = false;
            string fileIOStatusMessage;

            DisplayScreenHeader("Theme Select Menu");

            Console.WriteLine($"\tCurrent foreground color: {Console.ForegroundColor}");
            Console.WriteLine($"\tCurrent background color: {Console.BackgroundColor}");
            Console.WriteLine();

            Console.Write("\tWould you like to change the current theme, yes or no?: ");
            if (Console.ReadLine().ToLower() == "no")
            {
                do
                {
                    themeColors.foregroundColor = UserConsoleColor("foreground");
                    themeColors.backgroundColor = UserConsoleColor("background");

                    //
                    // set new theme
                    //
                    Console.ForegroundColor = themeColors.foregroundColor;
                    Console.BackgroundColor = themeColors.backgroundColor;
                    Console.Clear();
                    DisplayScreenHeader("Set Application Theme");
                    Console.WriteLine($"\tNew foreground color: {Console.ForegroundColor}");
                    Console.WriteLine($"\tNew background color: {Console.BackgroundColor}");

                    Console.WriteLine();
                    Console.Write("\tIs this the theme you would like?: ");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        themeChosen = true;
                        fileIOStatusMessage = WriteThemeExceptions(themeColors.foregroundColor, themeColors.backgroundColor);
                        if (fileIOStatusMessage == "Complete")
                        {
                            Console.WriteLine("\tNew theme written to data file.");
                        }
                        else
                        {
                            Console.WriteLine("\tNew theme not written to data file.");
                            Console.WriteLine($"\t{fileIOStatusMessage}");
                        }
                    }

                } while (!themeChosen);
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// get a console color from the user
        /// </summary>
        /// <param name="property">foreground or background</param>
        /// <returns>user's console color</returns>
        static ConsoleColor UserConsoleColor(string property)
        {
            ConsoleColor consoleColor;
            bool validAns;

            do
            {
                Console.Write($"\tEnter a value for the {property}: ");
                validAns = Enum.TryParse<ConsoleColor>(Console.ReadLine(), true, out consoleColor);

                if (!validAns)
                {
                    Console.WriteLine("\tYou have entered an invalid option. Please try again.");
                }
                else
                {
                    validAns = true;
                }
            } while (!validAns);

            return consoleColor;
        }

        /// <summary>
        /// read the theme exceptions
        /// </summary>
        /// <returns>tuple of foreground and background</returns>
        static (ConsoleColor foregroundColor, ConsoleColor backgroundColor) ReadThemeDataExceptions(out string fileIOStatusMessage)
        {
            string dataPath = @"Data/Theme.txt";
            string[] themeColors;

            ConsoleColor foregroundColor = ConsoleColor.White;
            ConsoleColor backgroundColor = ConsoleColor.Black;

            try
            {
                themeColors = File.ReadAllLines(dataPath);
                if (Enum.TryParse(themeColors[0], true, out foregroundColor) &&
                    Enum.TryParse(themeColors[1], true, out backgroundColor))
                {
                    fileIOStatusMessage = "Complete";
                }
                else
                {
                    fileIOStatusMessage = "Data file incorrectly formated.";
                }
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the folder for the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to read data file.";
            }

            return (foregroundColor, backgroundColor);
        }

        /// <summary>
        /// write theme info to a data file
        /// </summary>
        /// <returns>tuple of foreground and background</returns>
        static string WriteThemeExceptions(ConsoleColor foreground, ConsoleColor background)
        {
            string dataPath = @"Data/Theme.txt";
            string fileIOStatusMessage = "";

            try
            {
                File.WriteAllText(dataPath, foreground.ToString() + "\n");
                File.AppendAllText(dataPath, background.ToString());
                fileIOStatusMessage = "Complete";
            }
            catch (DirectoryNotFoundException)
            {
                fileIOStatusMessage = "Unable to locate the folder for the data file.";
            }
            catch (Exception)
            {
                fileIOStatusMessage = "Unable to write to data file.";
            }

            return fileIOStatusMessage;
        }
        #endregion
    }
}

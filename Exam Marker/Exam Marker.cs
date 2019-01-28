/*
 Project: INFO 1112 Final Project
 File: Form1.cs
 Name: LiWen Tan
 Section: 11
 Purpose: This program marks student exam in multiple choice format, and displays the exam results.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System. IO;

namespace Exam_Marker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        char[] answer = {'A','B','D','A','C',
                         'C','A','A','B','A'};
        string wrong;

        //variable for timer
        int timerCount = 0;
        
        private void clearBtn_Click(object sender, EventArgs e)
        {
            outputLbl.Text = "";
            timeCompleted.Text = "";
            correctanswerListBox.Items.Clear();
            wronganswerListBox.Items.Clear();
            question1TextBox.Clear();
            question2TextBox.Clear();
            question3TextBox.Clear();
            question4TextBox.Clear();
            question5TextBox.Clear();
            question6TextBox.Clear();
            question7TextBox.Clear();
            question8TextBox.Clear();
            question9TextBox.Clear();
            question10TextBox.Clear();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void questionButton_Click(object sender, EventArgs e)
        {

            StreamReader inputFile;
            inputFile = File.OpenText("ExamQuestions.txt");
            String examQuestions; 
         
            while (!inputFile.EndOfStream)
            {
                //Get the file into the list box 
                examQuestions = inputFile.ReadLine();

                //Add to list box 
                examQuestionsListBox.Items.Add(examQuestions); 
            }
            inputFile.Close();
            
        }

        private void submitAnswersButton_Click(object sender, EventArgs e)
        {
            try
            {

                const int SIZE = 10;
                //Stops the timer
                timerCounter.Stop();

                String[] numbersArray = new String[SIZE];

                //Get user answers 
                numbersArray[0] = question1TextBox.Text;
                numbersArray[1] = question2TextBox.Text;
                numbersArray[2] = question3TextBox.Text;
                numbersArray[3] = question4TextBox.Text;
                numbersArray[4] = question5TextBox.Text;
                numbersArray[5] = question6TextBox.Text;
                numbersArray[6] = question7TextBox.Text;
                numbersArray[7] = question8TextBox.Text;
                numbersArray[8] = question9TextBox.Text;
                numbersArray[9] = question10TextBox.Text;

                StreamWriter outputFile;

                //Create file 
                outputFile = File.CreateText("userQuestions.Txt");

                //Write into array 
                for (int index = 0; index < numbersArray.Length; index++)
                {
                    outputFile.WriteLine(numbersArray[index]);

                }
                outputFile.Close();
                markExam();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void markExam()
        {
            // Declare a value of right questions
            decimal right = 0;
            int index = 0; ;
            // get information in text
            StreamReader inputFile;
            inputFile = File.OpenText("userQuestions.txt");

            while (index < answer.Length && !inputFile.EndOfStream)
            {

                if (answer[index].ToString() == inputFile.ReadLine())
                {
                    right++;
                    correctanswerListBox.Items.Add("\n" + "question" + (index + 1) + " " + answer[index]);
                }
                else
                {
                    wrong += "\n" + "question" + (index + 1);
                    wronganswerListBox.Items.Add("\n" + "question" + (index + 1) + " " + answer[index]);
                }
                index++;
            }
            inputFile.Close();

            //Varible for amount of questions
            const int NUM_QUESTIONS = 10;
            // Result display in the label box
            outputLbl.Text = "Number of right answer is " +
                right + " \n" + "Number of wrong answer is " +
                ( NUM_QUESTIONS - right) + " \n" + "The questions you answered wrong are: " + "\n" + wrong;
            // Percentage messagebox 
            decimal percentage;     // declare a value

            percentage = right / 10;
            MessageBox.Show("The percentage of your score is " + percentage.ToString("p"));

            resultsExam(percentage);
            timeExamCompleted();
        }

        private void timeExamCompleted()
        {
            MessageBox.Show("Time it took to finish exam : " + timerCount + " seconds.");
        }

        private void resultsExam(decimal percentage)
        {
            decimal word; // declare a value
            word = percentage;

            // Comment (display with value "word")
            if (word * 100 == 100) //Grade A+
            {
                MessageBox.Show("Comment: You are brilliant. You received an A+!");
            }
            else if (word * 100 >= 90)//grade A
            {
                MessageBox.Show("Comment: You are clever. You received a A");
            }
            else if (word * 100 >= 80)//B
            {
                MessageBox.Show("Comment: Well done! You received a B!");
            }
            else if (word * 100 >= 70)//B-
            {
                MessageBox.Show("Comment: Not bad! You received a B-");
            }
            else if (word * 100 >= 60)//c
            {
                MessageBox.Show("Comment: Need improve. You received a C");
            }
            else if (word == 0)//F
            {
                MessageBox.Show("Comment: I don't know what I should say. Failed the Exam F");
            }
            else
            {
                MessageBox.Show("Comment: See you after school. You received a F");
            }

            // whether student pass or not
            if (percentage * 100 >= 60)
            {
                MessageBox.Show("Congratualation! You pass the test.");
            }
            else
            {
                MessageBox.Show("Sorry, you do not pass the test.");
            }
        }

        private void timerCounter_Tick(object sender, EventArgs e)
        {
            //Increment the timer
            timerCount++;
            timeCompleted.Text = timerCount.ToString() + " seconds";
            
        }
    }}

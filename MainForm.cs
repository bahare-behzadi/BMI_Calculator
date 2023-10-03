using BMI_calculator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMICalculator
{
    public partial class MainForm : Form
    {
        // Dfining the variables of Mainform
        private BmiCalculator bmiCal = new BmiCalculator();
        private SavingCalculator savingCal = new SavingCalculator();
        private BmrCalculator bmrCal = new BmrCalculator();
        private string name = string.Empty;
        // Definig the constructor 
        public MainForm()
        {
            InitializeComponent();
            InitializeGUI();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        // This method is written for initializing the from
        private void InitializeGUI()
        {
            this.Text = this.Text + " by Bahareh Behzadi";
            labelBmiResult.Text = string.Empty;
            labelWeightCategory.Text = string.Empty;
            radioButtonMetric.Checked = true;
            labelAmountPaidResult.Text = string.Empty;
            labelBalanceResult.Text = string.Empty;
            radioButtonFemale.Checked = true;
        }
        // This method is written because we have two different methods for measuring and this
        // will help us to seprate them
        private void UpdateHeightText()
        {
            if (radioButtonMetric.Checked)
            {
                labelHeight.Text = "Height (cm)";
                labelWeight.Text = "Weight (kg)";
                bmiCal.SetUnitType(UnitTypes.Metric);
                textBoxInc.Visible = false;
            }
            else
            {
                labelHeight.Text = "Height (ft, in)";
                labelWeight.Text = "Weight (lbs)";
                bmiCal.SetUnitType(UnitTypes.Imperial);
                textBoxInc.Visible = true;
            }
            labelBmiResult.Text = string.Empty;
            labelWeightCategory.Text = string.Empty;
        }

        private void radioButtonMetric_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHeightText();
        }

        private void radioImperial_CheckedChanged(object sender, EventArgs e)
        {
            UpdateHeightText() ;
        }
        // this method will calculate BMI and display it
        private void buttonCalculation_Click(object sender, EventArgs e)
        {
            bool ok = ReadName() && ReadHeight() && ReadWeight();
            ReadUnit();
            if (ok)
            {
                CalculateAndDisplay();
            }
        }
        // This method will define which units we should use for measuring BMI. 
        //Metrik which is kg & cm or Imperial which is lb and inch
        private void ReadUnit()
        {
            if (radioButtonMetric.Checked)
                bmiCal.SetUnitType(UnitTypes.Metric);
            else
                bmiCal.SetUnitType(UnitTypes.Imperial);
        }
        private void CalculateAndDisplay()
        {
            double bmi = bmiCal.CalculatorBMI();
            labelBmiResult.Text = bmi.ToString("0.00");// This shows the result with two numbers as a fraction
            labelWeightCategory.Text = bmiCal.BmiWeightCategory();
            Normalweight();
        }
        //This method read the name and delete the extra spaces or shows error if no data is entered
        private bool ReadName()
        {
            bool correctInput = false;
            name = textBoxName.Text.Trim();
            if (name.Length != 0)
            {
                groupBoxResults.Text = groupBoxResults.Text + " " + name;
                correctInput = true;
            }
            else
            {
                MessageBox.Show("The name is invalid (empty name)!");
            }
            return correctInput;
        }
        //This method read the height and cahnge it to number and shows error if no data is entered
        private bool ReadHeight()
        {
            double height = 0.0;
            double inch = 0.0;
            bool correctInput = double.TryParse(textBoxHeight.Text, out height);
            if (!correctInput) 
            {
                MessageBox.Show("The height value is invalid!", "Error");
            }

            if (radioImperial.Checked)
            {
                correctInput = correctInput && double.TryParse(textBoxInc.Text, out inch);
                
                if (!correctInput)
                {
                    MessageBox.Show("The inch value is invalid!", "Error");
                }
            }
            if (bmiCal.GetUnitType() == UnitTypes.Metric)
                height = height / 100.0; // this changes centimeter to meter
            else
                height = height * 12.0 + inch; // this changes foot to inch
            bmiCal.SetHeight(height);
            return correctInput;
        }
        //This method read the weight and cahnge it to number and shows error if no data is entered
        private bool ReadWeight()
        {
            double weight = 0.0;

            bool correctInput = double.TryParse(textBoxWeight.Text, out weight);
            if ((!correctInput) || (weight < 0))
            {
                MessageBox.Show("The weghit value is invalid!", "Error");
            }
            bmiCal.SetWeight(weight);
            return correctInput;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }
        //This method let the program count the normal weight 
        private void Normalweight()
        {
            string weightUnit = string.Empty;
            bmiCal.NormalWeight();

            if (bmiCal.GetUnitType() == UnitTypes.Metric)
            {
                weightUnit = "kg";
            }
            else
            {
                weightUnit = "lb";
            }
            labelNormalWeight.Text = labelNormalWeight.Text + " " + bmiCal.GetLowWeightLimit().ToString("0.00") + weightUnit  + " and " + bmiCal.GetHighWeightLimit().ToString("0.00") + weightUnit;
        }
        private void groupBoxUnits_Enter(object sender, EventArgs e)
        {

        }

        private void labelInterest_Click(object sender, EventArgs e)
        {

        }
        //This method is for getting error if the inputs are not appropriate 
        private double ReadDouble(String str, out bool success)
        {
            double value = -1.00;
            success = false;
            if (double.TryParse(str, out value))
            {
                success = true;
            }
            return value;
        }
        // The below methods are for getting data and assess the correctness of data which are entered
        private bool ReadSavingMonthlyDeposit()
        {
            bool success = true;
            double monthlyDeposit = ReadDouble(textBoxMonthlyDeposit.Text, out success);
            if (success)
            {
                savingCal.SetMonthlyDeposit(monthlyDeposit);
            }
            else
            {
                MessageBox.Show("Invalid value for monthly deposit!");
            }
            return success;
        }
        private bool ReadInitialDeposit()
        {
            bool success = true;
            double initialDeposit = ReadDouble(textBoxinitialDeposit.Text, out success);
            if (success)
            {
                savingCal.SetInitialDeposit(initialDeposit);
            }
            else
            {
                MessageBox.Show("Invalid value for initial deposit!");
            }
            return success;
        }
        private bool ReadPeriod()
        {
            bool success = true;
            double period = ReadDouble(textBoxPeriods.Text, out success);
            if (success)
            {
                savingCal.SetPeriod(period);
            }
            else
            {
                MessageBox.Show("Invalid value for period!");
            }
            return success;
        }
        private bool ReadInterest()
        {
            bool success = true;
            double interest = ReadDouble(textBoxInterest.Text, out success);
            if (success)
            {
                savingCal.SetInterest(interest);
            }
            else
            {
                MessageBox.Show("Invalid value for interest!");
            }
            return success;
        }
        private bool ReadFees()
        { 
            bool success = true;
            double fees = ReadDouble(textBoxFees.Text, out success);
            if (success)
            {
                savingCal.SetFees(fees);
            }
            else
            {
                MessageBox.Show("Invalid value for fees!");
            }
            return success;
        }
        // Messuring and Displaying the results

        private void buttonSavingCalculate_Click(object sender, EventArgs e)
        {
            if (ReadFees() && ReadInterest() && ReadPeriod() && ReadInitialDeposit() && ReadSavingMonthlyDeposit())
            {
                savingCal.CalculatingSavingParams();
                labelAmountPaidResult.Text = savingCal.GetamountPaid().ToString("0.00");
                labelAmountEarnedResult.Text = savingCal.GetamountEarned().ToString("0.00");
                labelBalanceResult.Text = savingCal.GetfainalBalance().ToString("0.00");
                labelTotalResult.Text = savingCal.GettotalFees().ToString("0.00");
            }
            else
            {
                MessageBox.Show("One of the parameters is wrong");
            }
        }
        // Reading and assessing data for messuring BMR
        private bool ReadAge()
        {
            int age = 0;
            bool correctInput = int.TryParse(textBoxAge.Text, out age);
            if ((!correctInput) || (age < 0))
            {
                MessageBox.Show("Invalid age!!!");
            }
            bmrCal.SetAge(age);
            return correctInput;
        }
        private bool ReadWeeklyActivity()
        {
            double factor = 0;
            bool correctInput = true;
            if (radioButtonSedentary.Checked)
                factor = 1.2;
            else if (radioButtonLightly.Checked)
                factor = 1.375;
            else if (radioButtonModeratly.Checked)
                factor = 1.550;
            else if (radioButtonVery.Checked)
                factor = 1.725;
            else if (radioButtonhard.Checked)
                factor = 1.9;
            else
            {
                MessageBox.Show("One of the activities must be chosen!!!");
                correctInput = false;   
            }
                
            listBoxResult.Text = string.Empty;
            bmrCal.SetFactor(factor);
            return correctInput;
        }
        private bool ReadGender()
        {
            bool correctInput = true;
            if (radioButtonFemale.Checked)
                bmrCal.SetGender(GenderTypes.Female);
            else if (radioButtonmale.Checked)
                bmrCal.SetGender(GenderTypes.male);
            else
            {
                MessageBox.Show("One of the Genders should be chosen!!!");
                correctInput = false;
            }
            return correctInput;
        }
        private bool ReadBrmWeight()
        {
            ReadWeight();
            if (radioButtonMetric.Checked)
                bmrCal.SetWeight(bmiCal.GetWeight());
            else
                bmrCal.SetWeight(bmiCal.GetWeight() * .45359);
            return ReadWeight();
        }
        private bool ReadBrmHeight()
        {
            ReadHeight();
            if (radioButtonMetric.Checked)
                bmrCal.SetHeight(bmiCal.GetHeight()*100);
            else
                bmrCal.SetHeight(bmiCal.GetHeight() * 2.54);
            return ReadHeight();
        }
        // Calculating and shoeing the BMR results
        private void BMRCalculator_Click(object sender, EventArgs e)
        {
            bool ok = ReadAge() && ReadGender() && ReadWeeklyActivity() && ReadName() && ReadBrmWeight() && ReadBrmHeight();
            if (ok)
            {
                CalculateAndDisplayBmr();
            }
        }
        private void CalculateAndDisplayBmr()
        {
            
            listBoxResult.Items.Clear();
            listBoxResult.Items.Add($"BRM Results for {name}");
            listBoxResult.Items.Add("\n\n");
            listBoxResult.Items.Add($"Your BMR (caleries/day)                    {bmrCal.CalculatBmr().ToString("0.0")}");
            listBoxResult.Items.Add($"colories to maintain your weight           {bmrCal.KeepWeight().ToString("0.0")}");
            listBoxResult.Items.Add($"colories to loos 1kg per week              {(bmrCal.KeepWeight()-500).ToString("0.0")}");
            listBoxResult.Items.Add($"colories to gain .5kg per week             {(bmrCal.KeepWeight()+500).ToString("0.0")}");
            listBoxResult.Items.Add($"colories to gain 1kg per week              {(bmrCal.KeepWeight()+1000).ToString("0.0")}");
            listBoxResult.Items.Add("\n\n");
            listBoxResult.Items.Add($"loosing more than 1000 colories per day is to be avoided");


        }
        private void groupBoxGender_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxBMR_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxBMRPram_Enter(object sender, EventArgs e)
        {

        }

        private void radioButtonFemale_CheckedChanged(object sender, EventArgs e)
        {
            listBoxResult.Items.Clear();
        }

        private void radioButtonmale_CheckedChanged(object sender, EventArgs e)
        {
            listBoxResult.Items.Clear();
        }

        private void labelAge_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonSedentary_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxweekly_Enter(object sender, EventArgs e)
        {

        }

        private void radioButtonLightly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonModeratly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonVery_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonhard_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    class BmiCalculator
    {
        //Defining the private data for messuring the BMI
        private double height;
        private double weight;
        private UnitTypes unitType;
        private double lowWeightLimit = 0.0;
        private double highWeightLimit = 0.0;

        public BmiCalculator()
        {
            unitType = UnitTypes.Metric;
        }
        //Defining the Setter And Getters methods
        //These methods are used for using the data. Variables of this class is private and we should call the get or set methods to get access to this data.
        #region Setter & getter
        public double GetHeight()
        { 
            return height;
        }
        public double GetWeight()
        {
            return weight;
        }
        public void SetHeight(double height)
        {
            if (height >= 0) 
            {
                this.height = height;
            }
        }
        public void SetWeight(double weight)
        {
            if (weight >= 0)
            {
                this.weight = weight;
            }
        }
        public UnitTypes GetUnitType()
        {
            return unitType;
        }
        public void SetUnitType(UnitTypes unitType)
        { 
            this.unitType = unitType;
        }
        public double GetLowWeightLimit()
        {
            return lowWeightLimit;
        }
        public double GetHighWeightLimit()
        {
            return highWeightLimit;
        }
        #endregion
        //This method will calculate the BMI and return the BMI
        public double CalculatorBMI()
        {
            double bmiValue = 0.0;
            double factor = 1.0;
            if (unitType == UnitTypes.Imperial)
            {
                factor = 703.0;
            }
            bmiValue = factor * weight / (height * height);
            return bmiValue;    
        }
        public string BmiWeightCategory()
        {
            double bmi = CalculatorBMI();
            string result = string.Empty;
            if (bmi < 18.5)
                result = "Under weight";
            else if (bmi <= 24.9)
                result = "Normal weight";
            else if (bmi <= 29.9)
                result = "Over weight (Pre_obesity";
            else if (bmi <= 34.9)
                result = "Overweight (Obesity class I)";
            else if (bmi <= 39.9)
                result = "Overweight (Obesity class II)";
            else
                result = "Overweight (Obesity class III)";
            return result;
        }
        //This method will calculate the Normal weight
        public void NormalWeight()
        {
            if (unitType == UnitTypes.Imperial)
            {
                lowWeightLimit = (18.5 * height * height) / 703.0;//18.5 is the lowest BMI for a normal weight
                highWeightLimit = (24.9 * height * height) / 703.0;//24.9 is the lowest BMI for a normal weight
            }
            else
            {
                lowWeightLimit = (18.5 * height * height);
                highWeightLimit = (24.9 * height * height);
            }
        }
    }

}

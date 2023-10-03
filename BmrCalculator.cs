using BMICalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator
{
    internal class BmrCalculator
    {
        //Defining the private data for messuring the BNR
        private int age;
        private GenderTypes genderType;
        private double factor = 0.0;
        private BmiCalculator bmical = new BmiCalculator();
        private double weight = 0;
        private double height = 0;

        public BmrCalculator()
        {
            genderType = GenderTypes.Female;
        }
        //Defining the Setter And Getters methods
        //These methods are used for using the data. Variables of this class is private and we should call the get or set methods to get access to this data.
        #region Geter and Seter
        public GenderTypes GetGender()
        {
            return genderType;
        }
        public void SetGender(GenderTypes genderType)
        {
            this.genderType = genderType;
        }
        public double GetFactor()
        {
            return factor; 
        }
        public void SetFactor(double factor) 
        { 
            this.factor = factor; 
        }
        public int GetAge()
        {
            return age;
        }
        public void SetAge(int age)
        {
            if (age > 0) 
                this.age = age;
        }
        public void SetWeight(double weight)
        {
            this.weight= weight;
        }
        public double Getweight()
        { return weight; }
        public void SetHeight(double height)
        { this.height = height; }
        public double GetHeight()
        {  return height; }
        #endregion
        //This method will calculate the BMR and return the BMR
        public double CalculatBmr()
        {
            double bmr = 0;
            bmr = 10 * weight + 6.25 * height - 5 * age;//this is the formula for calculating BMR
            if (genderType == GenderTypes.Female)
            {
                bmr -= 161;//If the user choose the woman then BMR = BMR - 161
            }
            else
            {
                bmr += 5;//If the user choose the woman then BMR = BMR + 5
            }
            return bmr;
        }
        public double KeepWeight()
        {
            double maintainWeightBMR = 0;
            maintainWeightBMR = CalculatBmr()*factor;
            return maintainWeightBMR;
        }
    }
}

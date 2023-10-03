using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI_calculator
{
    internal class SavingCalculator
    {
        //Defining the variables 
        private double initialDeposit = 0;
        private double monthlyDeposit = 0;
        private double period = 0;
        private double interest = 0;
        private double fees = 0;
        private double amountPaid= 0;
        private double amountEarned= 0;
        private double fainalBalance= 0;
        private double totalFees = 0;
        public SavingCalculator()
        {
        }
        //Setter and Getter methods
        #region Get and Set methods
        public double GetInitialDeposit()
        { 
            return initialDeposit; 
        }
        public void SetInitialDeposit(double initialDeposit)
        {
            if (initialDeposit > 0)
            {
                this.initialDeposit = initialDeposit;
            }
        }
        public double GetMonthlyDeposit()
        {
            return monthlyDeposit;
        }
        public void SetMonthlyDeposit(double monthlyDeposit)
        {
            if (initialDeposit > 0)
            {
                this.monthlyDeposit = monthlyDeposit;
            }
        }
        public double GetPeriod()
        {
            return period;
        }
        public void SetPeriod(double period)
        {
            if (period > 0)
            {
                this.period = period;
            }
        }
        public double GetInterest()
        {
            return interest;
        }
        public void SetInterest(double interest)
        {
            if (interest > 0)
            {
                this.interest = interest;
            }
        }
        public double GetFees()
        {
            return fees;
        }
        public void SetFees(double fees)
        {
            if (fees > 0)
            {
                this.fees = fees;
            }
        }
        public double GetamountPaid()
        { return amountPaid; }
        public double GetamountEarned()
        { return amountEarned; }
        public double GetfainalBalance()
        { return fainalBalance; }
        public double GettotalFees()
        { return totalFees; }
        #endregion
        //Calculating The variables such as final balance and amount earned and ...
        public void CalculatingSavingParams()
        {
            double numOfMonths = period * 12;
            amountPaid = initialDeposit + (monthlyDeposit * numOfMonths);
            double balance = initialDeposit;
            double interestRate = interest / 100;
            for (int month = 1; month <= numOfMonths; month++)
            {
                double interestEarned = balance * interestRate / 12; 
                balance += interestEarned + monthlyDeposit;
            }
            fainalBalance = balance;
            amountEarned = fainalBalance - amountPaid;
            totalFees = fainalBalance * fees /100;
        }
    }
}

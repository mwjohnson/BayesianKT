using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BayesianKT
{
    public class BKTViewModel
    {
        Collection<bool> ResponseVector = new Collection<bool>();
        Collection<double> ModelValues = new Collection<double>();

        public double pSlip { get; set; }
        public double pGuess { get; set; }
        public double pTrained { get; set; }
        public double pLearned { get; set; }

        public BKTViewModel()
        {
            pSlip = 0.1;
            pGuess = 0.3;
            pTrained = 0.1;
            pLearned = 0.5;
        }

        public void ConstructResponseVector(String input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                /*if (input[i].CompareTo('0') == 0)
                    ResponseVector.Add(false);
                else
                    ResponseVector.Add(true);*/

                ResponseVector.Add(input[i].CompareTo('0') != 0);
            }
        }

        public Collection<bool> RetreiveResponses()
        {
            return (ResponseVector);
        }

        public void SetParameters(double pL, double pG, double pS, double pT)
        {
            pLearned = pL;
            pGuess = pG;
            pSlip = pS;
            pTrained = pT;
        }

        internal String PostOutputValues()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("L" + pLearned + ",S" + pSlip + ",G" + pGuess + ",T" + pTrained);
            output.AppendLine(pLearned.ToString());
            for (int i = 0; i < ModelValues.Count; i++)
            {
                output.AppendLine(ModelValues.ElementAt(i).ToString());
            }
            return (output.ToString());
        }

        public Collection<double> RetreiveOutputvalues()
        {
            return (ModelValues);
        }

        public void BKTrace()
        {
            double k = 0.0;
            double Ktm1 = pLearned; //Ktm1 is K at time t, minus 1. Or K_t-1

            for (int i = 0; i < ResponseVector.Count; i++)
            {
                /*if (ResponseVector.ElementAt(i))
                {
                    k = Ktm1 * (1 - pSlip) / (Ktm1 * (1 - pSlip) + pGuess * (1 - Ktm1));
                }
                else
                {
                    k = (Ktm1 * pSlip) / (Ktm1 * pSlip + (1 - Ktm1) * (1 - pGuess));
                }*/

                k = (ResponseVector.ElementAt(i)) ?
                    Ktm1 * (1 - pSlip) / (Ktm1 * (1 - pSlip) + pGuess * (1 - Ktm1)) :
                    (Ktm1 * pSlip) / (Ktm1 * pSlip + (1 - Ktm1) * (1 - pGuess));

                double Kt = k + (1 - k) * pTrained;
                Ktm1 = Kt;
                ModelValues.Add(Kt);
            }
            PostOutputValues();
        }

        internal void Clear()
        {
            //ResponseVector = new LinkedList<bool>();
            //ModelValues = new LinkedList<double>();
            ResponseVector.Clear();
            ModelValues.Clear();
        }
    }
}

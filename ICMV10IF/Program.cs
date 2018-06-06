using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMV10IF
{
    class Program
    {
        static void Main(string[] args)
        {
            ICMV10API myICMV10API = new ICMV10API("https://spm.ibmcloud.com", "TMobileDemo", "AutoTesting", "UATP@ssword");
            var Calcs = myICMV10API.GetCalculations();
            var Result = myICMV10API.GetDataFromCalc("449", "PayeeID_=AE001;Periods=2017-01 JAN;AttributeID=KPI YTD PAYOUT [ACTUALS]");
        }
    }
}

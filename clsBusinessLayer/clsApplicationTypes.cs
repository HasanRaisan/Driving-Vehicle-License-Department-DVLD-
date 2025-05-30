using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using static System.Net.Mime.MediaTypeNames;


namespace BusinessLayer
{
    public class clsApplicationTypes
    {
        public decimal ApplicationFees { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationTypeTitle { get; set; }
      

        static public DataTable GetAllApplictionTypes()
        {
            return clsApplicationTypesDataAceess.GetAllApplicationTypes();
        }


        public clsApplicationTypes()
        {

        }
        private clsApplicationTypes(int ApplicationID, decimal ApplicationFees, string ApplicationTypeTitle)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
        }
        

        public bool _UpdateApplicationTypeFee()
        {
            return clsApplicationTypesDataAceess.UpdateApplicationType(this.ApplicationID, this.ApplicationFees, this.ApplicationTypeTitle);
        }


        static public clsApplicationTypes FindApplication(int ApplicationID)
        {

            decimal ApplicationFees = 0;
            string ApplicationTypeTitle = "";
           
            if(clsApplicationTypesDataAceess.FindApplication(ApplicationID,ref ApplicationFees, ref ApplicationTypeTitle))
            {
                return new clsApplicationTypes(ApplicationID, ApplicationFees, ApplicationTypeTitle);
            }
            else return null;
           


        }



        public bool Save()
        {
           return _UpdateApplicationTypeFee(); ;
        }


    }



}

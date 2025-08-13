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
    public class clsApplicationType
    {
        public decimal ApplicationFees { get; set; }
        public int ApplicationID { get; set; }
        public string ApplicationTypeTitle { get; set; }

        private enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode { get; set; } = enMode.AddNew;



        static public DataTable GetAllApplictionTypes()
        {
            return clsApplicationTypesDataAceess.GetAllApplicationTypes();
        }


        public clsApplicationType()
        {
            this.ApplicationFees = 0;
            this.ApplicationID = -1;
            this.ApplicationTypeTitle = "";
            this._Mode = enMode.AddNew;
        }
        private clsApplicationType(int ApplicationID, decimal ApplicationFees, string ApplicationTypeTitle)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationFees = ApplicationFees;
            this._Mode = enMode.Update;
        }
        

        public bool _UpdateApplicationTypeFee()
        {
            return clsApplicationTypesDataAceess.UpdateApplicationType(this.ApplicationID, this.ApplicationFees, this.ApplicationTypeTitle);
        }


        static public clsApplicationType FindApplication(int ApplicationID)
        {

            decimal ApplicationFees = 0;
            string ApplicationTypeTitle = "";
           
            if(clsApplicationTypesDataAceess.FindApplication(ApplicationID,ref ApplicationFees, ref ApplicationTypeTitle))
            {
                return new clsApplicationType(ApplicationID, ApplicationFees, ApplicationTypeTitle);
            }
            else return null;
        }

        [Obsolete("⚠️ Warning: This method is not yet implemented.", false)]
        static public int AddNewApplicationType(decimal ApplicationFees, string ApplicationTypeTitle)
        {
            throw new NotImplementedException("⚠️ Warning: This method is not yet implemented.");

            //int ApplicationID = clsApplicationTypesDataAceess.AddApplicationType(ApplicationFees, ApplicationTypeTitle);
            //if (ApplicationID > 0)
            //{
            //    return ApplicationID;
            //}
            //else
            //    return -1;
        }


        public bool Save()
        {
            switch (this._Mode)
            {
                case enMode.AddNew:

                    //if (AddNewApplicationType())
                    //{
                    //    this._Mode = enMode.Update;
                    //    return true;
                    //}
                    //else
                    //    return false;


                case enMode.Update: return _UpdateApplicationTypeFee();

                default: return false;

            }

        }


    }



}

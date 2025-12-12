# 🪪 DVLD Driving License Management System

A desktop application designed to manage the issuance and renewal of various types of driving licenses, providing an environment that facilitates handling driver data and requests.

---

## 🚀 Core Services

- 🆕 Issue a new driving license for the first time.
- ♻️ Renew an expired license.
- 🔁 Replace a lost or damaged license.
- 🌍 Issue an international driving license.
- 🔓 Release a suspended license.

---

## ⚙️ Technologies Used

- **C#** with **.NET Framework**
- **Windows Form**
- **SQL Server** for data management and storage

---

## 🎯 Features

- 🖥️ Simple graphical interface for adding drivers and managing all types of requests.
- ✅ Internal verification system to ensure requests meet conditions and regulations before execution.
- 📋 Clear data organization with easy browsing and searching.

---

## 🚀 Preview

- [Login](https://drive.google.com/file/d/1NAI7zFQeRH2M22WGtmpxVeOpDTv-RYv3/view?usp=drive_link)
- [Home](https://drive.google.com/file/d/1X6llV_rLHUgL0FV5qNlzEl2ImL92WQMK/view?usp=drive_link)
- [Driving License Service](https://drive.google.com/file/d/1Ii3e8dr7y8NkRQ8y1LufIpC_PpOs2i0Z/view?usp=drive_link)
- [Detain License](https://drive.google.com/file/d/1oznx6fQ1jrQbXT0_qRqsVcr07goRYKh1/view?usp=drive_link)
- [Drivers](https://drive.google.com/file/d/1PmtJmigdNjvdM8ZoB12QoNGd9bP3StHH/view?usp=drive_link)
- [Mange Users](https://drive.google.com/file/d/1B2DJ8vjlsVLtZkxxx9safmaLsqF23-GY/view?usp=drive_link)
- [New Local Driving License](https://drive.google.com/file/d/1d7tjHa3x_VBoMn0bT9teGyolFh8VBwNH/view?usp=drive_link)

---

## 📁 Folder Structure

### **DVLD**

```
DVLD
│   DVLD.sln
│   MainForm.cs
│   Program.cs
│
├── Applications
│   ├── International License
│   │       FormAddNewInternationlalLicense.cs
│   │       FormInternationalLicenseApplications.cs
│   │       UserControlShowInternationalLincenseApplicationDetails.cs
│   │
│   ├── Mange Application Types
│   │       FormApplicationsTypes.cs
│   │       FormEditApplicationsType.cs
│   │
│   ├── Mange Local Driving Applications
│   │       FormAddLocalLicense.cs
│   │       FormEditeLocalDrivingLicenseApplication.cs
│   │       FormShowApplicationDetails.cs
│   │       MangeLoacalDrivingLicenseApplications.cs
│   │       UserConShowLocalApplicationInfo.cs
│   │
│   ├── Release Detaind License
│   │       FormManageDetainLicense.cs
│   │       FormReleaseLicense.cs
│   │       Controls
│   │           UserControlDetainInfo.cs
│   │           UserControlReleaseLicense.cs
│   │
│   ├── Renew Local License
│   │       FormRenwedLicense.cs
│   │       UserControlShowRenwedApplicationsInfo.cs
│   │
│   └── Replacement For Lost or Damaged
│           FormReplacementForDamagedOrLostLicenses.cs
│           UserControlShowReplacementApplicationDetails.cs
│
├── Drivers
│       FormDrivers.cs
│
├── Driving Licenses Services
│       FormAddLocalLicense.cs
│
├── Global Classes
│       clsGlobal.cs
│       clsUtil.cs
│       clsValidatoin.cs
│
├── License
│   │   FormShowPersonLicensesHistory.cs
│   │
│   ├── Detain License
│   │       FormDetainLicense.cs
│   │
│   ├── International License
│   │       FormShowInternatonalLicenseInfo.cs
│   │       Controls
│   │           UserControlShowInternationalLicenseDetails.cs
│   │
│   └── Local License
│           FormIssuDrivingLicenseForTheFirstTime.cs
│           FormShowLicense.cs
│           Controls
│               UserControlDrivingLicenseInfo.cs
│
├── Login
│       FormLoginScreen.cs
│
├── People
│   │   AddEditPerson.cs
│   │   FormShowPersonDetails.cs
│   │   MangePeople.cs
│   │
│   └── Controls
│           ucnAddEitedPerson.cs
│           UserConShopPersonDetailWithFilter.cs
│           UserControlShowPersonDetails.cs
│
├── Tests
│   │   FormAddUpdateApointment.cs
│   │   FormListAppointmentTest.cs
│   │   FormTakeTest.cs
│   │
│   └── Test Types
│           FormEditTestType.cs
│           FormTestTypes.cs
│
└── Users
        FormAddEditUser.cs
        FormChangePassword.cs
        FormMangeUsers.cs
        FormShowUserDetails.cs
        UserControlUserDetails.cs

```

### **Business Layer**

```
clsBusinessLayer
│   clsApplication.cs
│   clsApplicationType.cs
│   clsCountry.cs
│   clsDetainLicense.cs
│   clsDriver.cs
│   clsInternationalLicense.cs
│   clsLicense.cs
│   clsLicenseClasses.cs
│   clsLicenseClassesBusinnessLayer.cs
│   clsLocalDrivingLicensesApplication.cs
│   clsLocalDrivingLicenseViews.cs
│   clsPerson.cs
│   clsTest.cs
│   clsTestAppointment.cs
│   clsTestType.cs
│   clsUser.cs
│   clsViews.cs
│   clsViewsBusinessLayer.cs
│   DVLD_Buisness.csproj

```

### **Data Access Layer**

```
clsDataAccessLayer
│   clsApplicationsDataAccess.cs
│   clsApplicationTypesDataAceess.cs
│   clsCountriesDataAccess.cs
│   clsDataAccessSettings.cs
│   clsDetainLicenseDataAccess.cs
│   clsDriverDataAccess.cs
│   clsGlobalDataAccess.cs
│   clsInternationalLicenseDataAccess.cs
│   clsLicenseClassDataAccess.cs
│   clsLicenseDataAccess.cs
│   clsLocalDrivingLicenseApplicationsDataAccess.cs
│   clsPersonDataAccess.cs
│   clsTestAppointmetsDataAccess.cs
│   clsTestsDataAccess.cs
│   clsTestTypeDataAccess.cs
│   clsUserDataAccess.cs
│   clsViewsDataAccess.cs
│   DVLD_DataAccess.csproj

```

### **Shared**

```
DVLD_Shared
│   ClsGlobal.cs
│   DVLD_Shared.csproj

```

### **People Pictures**

```
DVLD_Shared
│
│

```

---

## 📦 Notes

- The system is desktop-based and does not require an internet connection.
- Can be extended later to integrate with official traffic systems or e-services.

---

## 👨‍💻 Developer

- **Hassan Risan**
- hasan.raisann@gmail.com
- Full-Stack Developer

---

## 📜 License

This project is for educational or experimental purposes.

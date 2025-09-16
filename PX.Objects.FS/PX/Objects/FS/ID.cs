// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ID
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public static class ID
{
  public class LocationType
  {
    public const 
    #nullable disable
    string COMPANY = "CO";
    public const string CUSTOMER = "CU";
    public readonly string[] ID_LIST = new string[2]
    {
      "CO",
      "CU"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Company",
      "Customer"
    };
  }

  public class Condition
  {
    public const string NEW = "N";
    public const string USED = "U";
    public readonly string[] ID_LIST = new string[2]
    {
      "N",
      "U"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "New",
      "Used"
    };
  }

  public class OwnerType
  {
    public const string BUSINESS = "B";
    public const string EMPLOYEE = "E";
    public readonly string[] ID_LIST = new string[2]
    {
      "B",
      "E"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Business",
      "Staff Member"
    };
  }

  public class BillingRule
  {
    public const string TIME = "TIME";
    public const string FLAT_RATE = "FLRA";
    public const string NONE = "NONE";
    public readonly string[] ID_LIST = new string[3]
    {
      nameof (TIME),
      "FLRA",
      nameof (NONE)
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Time",
      "Flat Rate",
      "None"
    };
  }

  public class ContractPeriod_BillingRule
  {
    public const string TIME = "TIME";
    public const string FLAT_RATE = "FLRA";
    public readonly string[] ID_LIST = new string[2]
    {
      nameof (TIME),
      "FLRA"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Time",
      "Flat Rate"
    };
  }

  public class ContractPeriod_Actions
  {
    public const string SEARCH_BILLING_PERIOD = "SBP";
    public const string MODIFY_UPCOMING_BILLING_PERIOD = "MBP";
    public readonly string[] ID_LIST = new string[2]
    {
      "SBP",
      "MBP"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Search by Billing Periods",
      "Modify Upcoming Billing Period"
    };
  }

  public class ScheduleType
  {
    public const string AVAILABILITY = "A";
    public const string UNAVAILABILITY = "U";
    public readonly string[] ID_LIST = new string[2]
    {
      "A",
      "U"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Availability",
      "Unavailability"
    };
  }

  public class Schedule_FrequencyType
  {
    public const string DAILY = "D";
    public const string WEEKLY = "W";
    public const string MONTHLY = "M";
    public const string ANNUAL = "A";
    public readonly string[] ID_LIST = new string[4]
    {
      "D",
      "W",
      "M",
      "A"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Daily",
      "Weekly",
      "Monthly",
      "Yearly"
    };
  }

  public class Schedule_EntityType
  {
    public const string CONTRACT = "C";
    public const string EMPLOYEE = "E";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "E"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Contract",
      "Employee"
    };
  }

  public class PostDoc_EntityType
  {
    public const string APPOINTMENT = "AP";
    public const string SERVICE_ORDER = "SO";
    public readonly string[] ID_LIST = new string[2]
    {
      "AP",
      "SO"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Appointment",
      "Service Order"
    };
  }

  public class PostRegister_Type
  {
    public const string BillingProcess = "INVCP";
  }

  public class TimeConstants
  {
    public const int HOURS_12 = 720;
    public const int MINUTES_0 = 0;
    public const int MINUTES_10 = 10;
    public const int MINUTES_15 = 15;
    public const int MINUTES_30 = 30;
    public const int MINUTES_60 = 60;
  }

  public class WeekDays
  {
    public const string ANYDAY = "NT";
    public const string SUNDAY = "SU";
    public const string MONDAY = "MO";
    public const string TUESDAY = "TU";
    public const string WEDNESDAY = "WE";
    public const string THURSDAY = "TH";
    public const string FRIDAY = "FR";
    public const string SATURDAY = "SA";
    public readonly string[] ID_LIST = new string[8]
    {
      "NT",
      "SU",
      "MO",
      "TU",
      "WE",
      "TH",
      "FR",
      "SA"
    };
    public readonly string[] TX_LIST = new string[8]
    {
      "Any",
      "Sunday",
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday"
    };
  }

  public class WeekDaysNumber
  {
    public const int SUNDAY = 0;
    public const int MONDAY = 1;
    public const int TUESDAY = 2;
    public const int WEDNESDAY = 3;
    public const int THURSDAY = 4;
    public const int FRIDAY = 5;
    public const int SATURDAY = 6;
    public readonly int[] ID_LIST = new int[7]
    {
      0,
      1,
      2,
      3,
      4,
      5,
      6
    };
    public readonly string[] TX_LIST = new string[7]
    {
      "Sunday",
      "Monday",
      "Tuesday",
      "Wednesday",
      "Thursday",
      "Friday",
      "Saturday"
    };
  }

  public class Months
  {
    public const string JANUARY = "JAN";
    public const string FEBRUARY = "FEB";
    public const string MARCH = "MAR";
    public const string APRIL = "APR";
    public const string MAY = "MAY";
    public const string JUNE = "JUN";
    public const string JULY = "JUL";
    public const string AUGUST = "AUG";
    public const string SEPTEMBER = "SEP";
    public const string OCTOBER = "OCT";
    public const string NOVEMBER = "NOV";
    public const string DECEMBER = "DEC";
    public readonly string[] ID_LIST = new string[12]
    {
      "JAN",
      "FEB",
      "MAR",
      "APR",
      nameof (MAY),
      "JUN",
      "JUL",
      "AUG",
      "SEP",
      "OCT",
      "NOV",
      "DEC"
    };
    public readonly string[] TX_LIST = new string[12]
    {
      "January",
      "February",
      "March",
      "April",
      "May",
      "June",
      "July",
      "August",
      "September",
      "October",
      "November",
      "December"
    };
  }

  public class SourceType_ServiceOrder
  {
    public const string CASE = "CR";
    public const string OPPORTUNITY = "OP";
    public const string SALES_ORDER = "SO";
    public const string SERVICE_DISPATCH = "SD";
    public readonly string[] ID_LIST = new string[4]
    {
      "CR",
      "OP",
      "SO",
      "SD"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Case",
      "Opportunities",
      "SO Order",
      "FS Order"
    };
  }

  public class AppResizePrecision_Setup
  {
    public readonly int[] ID_LIST = new int[4]
    {
      10,
      15,
      30,
      60
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "10 MINUTES",
      "15 MINUTES",
      "30 MINUTES",
      "60 MINUTES"
    };
  }

  public class DfltCalendarViewMode_Setup
  {
    public const string VERTICAL = "VE";
    public const string HORIZONTAL = "HO";
    public readonly string[] ID_LIST = new string[2]
    {
      "VE",
      "HO"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Vertical",
      "Horizontal"
    };
  }

  public abstract class Priority_ALL
  {
    public const string LOW = "L";
    public const string MEDIUM = "M";
    public const string HIGH = "H";
    public readonly string[] ID_LIST = new string[3]
    {
      "L",
      "M",
      "H"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Low",
      "Medium",
      "High"
    };
  }

  public class Priority_ServiceOrder : ID.Priority_ALL
  {
  }

  public class Severity_ServiceOrder : ID.Priority_ALL
  {
  }

  public class LineType_ALL
  {
    public const string ALL = "<ALL>";
    public const string SERVICE = "SERVI";
    public const string INVENTORY_ITEM = "SLPRO";
    public const string SERVICE_TEMPLATE = "TEMPL";
    public const string NONSTOCKITEM = "NSTKI";
    public const string PICKUP_DELIVERY = "PU_DL";
    public const string LABOR_ITEM = "LABOR";
    public const string COMMENT = "CM_LN";
    public const string INSTRUCTION = "IT_LN";
    public readonly string[] ID_LIST_ALL = new string[6]
    {
      "SERVI",
      "TEMPL",
      "SLPRO",
      "NSTKI",
      "CM_LN",
      "IT_LN"
    };
    public readonly string[] TX_LIST_ALL = new string[6]
    {
      "Service",
      "Service Template",
      "Inventory Item",
      "Non-Stock Item",
      "Comment",
      "Instruction"
    };
  }

  public class LineType_AppSrvOrd : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[5]
    {
      "SERVI",
      "NSTKI",
      "SLPRO",
      "CM_LN",
      "IT_LN"
    };
    public new readonly string[] TX_LIST_ALL = new string[5]
    {
      "Service",
      "Non-Stock Item",
      "Inventory Item",
      "Comment",
      "Instruction"
    };
  }

  public class LineType_ServiceTemplate : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[5]
    {
      "SERVI",
      "NSTKI",
      "SLPRO",
      "CM_LN",
      "IT_LN"
    };
    public new readonly string[] TX_LIST_ALL = new string[5]
    {
      "Service",
      "Non-Stock Item",
      "Inventory Item",
      "Comment",
      "Instruction"
    };
  }

  public class LineType_Schedule : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[6]
    {
      "SERVI",
      "NSTKI",
      "TEMPL",
      "SLPRO",
      "CM_LN",
      "IT_LN"
    };
    public new readonly string[] TX_LIST_ALL = new string[6]
    {
      "Service",
      "Non-Stock Item",
      "Service Template",
      "Inventory Item",
      "Comment",
      "Instruction"
    };
  }

  public class LineType_Pickup_Delivery : ID.LineType_ALL
  {
    public readonly string[] ID_LIST_SERVICE = new string[1]
    {
      "PU_DL"
    };
    public readonly string[] TX_LIST_SERVICE = new string[1]
    {
      "Pickup/Delivery Item"
    };
  }

  public class LineType_ServiceContract : ID.LineType_ALL
  {
    public readonly string[] ID_LIST = new string[5]
    {
      "SERVI",
      "CM_LN",
      "IT_LN",
      "NSTKI",
      "TEMPL"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Service",
      "Comment",
      "Instruction",
      "Non-Stock Item",
      "Service Template"
    };
  }

  public class LineType_SalesPrice : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[3]
    {
      "SERVI",
      "SLPRO",
      "NSTKI"
    };
    public new readonly string[] TX_LIST_ALL = new string[3]
    {
      "Service",
      "Inventory Item",
      "Non-Stock Item"
    };
  }

  public class LineType_ContractPeriod : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[2]
    {
      "SERVI",
      "NSTKI"
    };
    public new readonly string[] TX_LIST_ALL = new string[2]
    {
      "Service",
      "Non-Stock Item"
    };
  }

  public class LineType_Profitability : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[4]
    {
      "SERVI",
      "NSTKI",
      "SLPRO",
      "LABOR"
    };
    public new readonly string[] TX_LIST_ALL = new string[4]
    {
      "Service",
      "Non-Stock Item",
      "Inventory Item",
      "Labor"
    };
  }

  public class LineType_InvLookup : ID.LineType_ALL
  {
    public new readonly string[] ID_LIST_ALL = new string[4]
    {
      "<ALL>",
      "SERVI",
      "SLPRO",
      "NSTKI"
    };
    public new readonly string[] TX_LIST_ALL = new string[5]
    {
      "All",
      "Service",
      "Non-Stock Item",
      "Inventory Item",
      "Labor"
    };
  }

  public class PriceType
  {
    public const string CONTRACT = "CONTR";
    public const string CUSTOMER = "CUSTM";
    public const string PRICE_CLASS = "PRCLS";
    public const string BASE = "BASEP";
    public const string DEFAULT = "DEFLT";
    public readonly string[] ID_LIST_PRICETYPE = new string[5]
    {
      "CONTR",
      "CUSTM",
      "PRCLS",
      "BASEP",
      "DEFLT"
    };
    public readonly string[] TX_LIST_PRICETYPE = new string[5]
    {
      "Contract",
      "Customer",
      "Customer Price Class",
      "Base",
      "Default"
    };
  }

  public class Status_SODet
  {
    public const string SCHEDULED_NEEDED = "SN";
    public const string SCHEDULED = "SC";
    public const string CANCELED = "CC";
    public const string COMPLETED = "CP";

    public static bool CanBeScheduled(string serviceStatus) => serviceStatus == "SN";
  }

  public class Status_AppointmentDet
  {
    public const string NOT_STARTED = "NS";
    public const string IN_PROCESS = "IP";
    public const string NOT_FINISHED = "NF";
    public const string NOT_PERFORMED = "NP";
    public const string CANCELED = "CC";
    public const string COMPLETED = "CP";
    public const string WaitingForPO = "WP";
    public const string RequestForPO = "RP";
  }

  public class Status_ServiceContract
  {
    public const string DRAFT = "D";
    public const string ACTIVE = "A";
    public const string SUSPENDED = "S";
    public const string CANCELED = "X";
    public const string EXPIRED = "E";
    public readonly string[] ID_LIST = new string[5]
    {
      "D",
      "A",
      "S",
      "X",
      "E"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Draft",
      "Active",
      "Suspended",
      "Canceled",
      "Expired"
    };
  }

  public class Status_ContractPeriod
  {
    public const string ACTIVE = "A";
    public const string PENDING = "P";
    public const string INACTIVE = "I";
    public const string INVOICED = "N";
    public readonly string[] ID_LIST = new string[4]
    {
      "A",
      "P",
      "I",
      "N"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Active",
      "Pending for Invoice",
      "Inactive",
      "Invoiced"
    };
  }

  public class Status_Route
  {
    public const string OPEN = "O";
    public const string IN_PROCESS = "P";
    public const string CANCELED = "X";
    public const string COMPLETED = "C";
    public const string CLOSED = "Z";
    public readonly string[] ID_LIST = new string[5]
    {
      "O",
      "P",
      "X",
      "C",
      "Z"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Open",
      "In Process",
      "Canceled",
      "Completed",
      "Closed"
    };
  }

  public class Status_Posting
  {
    public const string NOTHING_TO_POST = "NP";
    public const string PENDING_TO_POST = "PP";
    public const string POSTED = "PT";
    public readonly string[] ID_LIST = new string[3]
    {
      "NP",
      "PP",
      "PT"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Nothing to Post",
      "Pending to Post",
      "Posted"
    };
  }

  public class Status_Log
  {
    public const string IN_PROCESS = "P";
    public const string COMPLETED = "C";
    public const string PAUSED = "S";
    public readonly string[] ID_LIST = new string[3]
    {
      "P",
      "S",
      "C"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "In Process",
      "Paused",
      "Completed"
    };
  }

  public class LogActions
  {
    public const string START = "ST";
    public const string PAUSE = "PA";
    public const string RESUME = "RE";
    public const string COMPLETE = "CP";
    public readonly string[] ID_LIST = new string[4]
    {
      "ST",
      "PA",
      "RE",
      "CP"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Start",
      "Pause",
      "Resume",
      "Complete"
    };
  }

  public class StartLogActions : ID.LogActions
  {
    public new readonly string[] ID_LIST = new string[1]
    {
      "ST"
    };
    public new readonly string[] TX_LIST = new string[1]
    {
      "Start"
    };
  }

  public class PCRLogActions : ID.LogActions
  {
    public new readonly string[] ID_LIST = new string[3]
    {
      "PA",
      "RE",
      "CP"
    };
    public new readonly string[] TX_LIST = new string[3]
    {
      "Pause",
      "Resume",
      "Complete"
    };
  }

  public class FuelType_Equipment
  {
    public const string REGULAR_UNLEADED = "R";
    public const string PREMIUM_UNLEADED = "P";
    public const string DIESEL = "D";
    public const string OTHER = "O";
    public readonly string[] ID_LIST = new string[4]
    {
      "R",
      "P",
      "D",
      "O"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Regular Unleaded",
      "Premium Unleaded",
      "Diesel",
      "Other"
    };
  }

  public class Confirmed_Appointment
  {
    public const string ALL = "A";
    public const string CONFIRMED = "C";
    public const string NOT_CONFIRMED = "N";
    public readonly string[] ID_LIST = new string[3]
    {
      "A",
      "C",
      "N"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "<ALL>",
      "Confirmed",
      "Not Confirmed"
    };
  }

  public class PeriodType
  {
    public const string DAY = "D";
    public const string WEEK = "W";
    public const string MONTH = "M";
    public readonly string[] ID_LIST = new string[3]
    {
      "D",
      "W",
      "M"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Day",
      "Week",
      "Month"
    };
  }

  public class LicenseType_ValidIn
  {
    public const string ALL_LOCATIONS = "ALL";
    public const string COUNTRY_STATE_CITY = "CSC";
    public const string GEOGRAPHICAL_ZONE = "GZN";
  }

  public class ReasonType
  {
    public const string CANCEL_SERVICE_ORDER = "CSOR";
    public const string CANCEL_APPOINTMENT = "CAPP";
    public const string WORKFLOW_STAGE = "WSTG";
    public const string APPOINTMENT_DETAIL = "APPD";
    public const string GENERAL = "GNRL";
    public readonly string[] ID_LIST = new string[5]
    {
      "CSOR",
      "CAPP",
      "WSTG",
      "APPD",
      "GNRL"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Cancel Service Order",
      "Cancel Appointment",
      "Workflow Stage",
      "Appointment Detail",
      "General"
    };
  }

  public class Setup_CopyTimeSpentFrom
  {
    public const string NONE = "N";
    public const string ACTUAL_DURATION = "A";
    public const string ESTIMATED_DURATION = "E";
    public readonly string[] ID_LIST = new string[3]
    {
      "A",
      "E",
      "N"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Actual Service Duration",
      "Estimated Service Duration",
      "None"
    };
  }

  public class ContractType_BillingFrequency
  {
    public const string EVERY_4TH_MONTH = "F";
    public const string SEMI_ANNUAL = "S";
    public const string ANNUAL = "A";
    public const string BEG_OF_CONTRACT = "B";
    public const string END_OF_CONTRACT = "E";
    public const string DAYS_30_60_90 = "D";
    public const string TIME_OF_SERVICE = "T";
    public const string NONE = "N";
    public const string MONTHLY = "M";
    public readonly string[] ID_LIST = new string[9]
    {
      "F",
      "S",
      "A",
      "B",
      "E",
      "D",
      "T",
      "N",
      "M"
    };
    public readonly string[] TX_LIST = new string[9]
    {
      "Every 4th Month",
      "Semi Yearly",
      "Yearly",
      "Beg. of Contract",
      "End of Contract",
      "30/60/90 Days",
      "Time of Service",
      "None",
      "Monthly"
    };
  }

  public class Contract_BillTo
  {
    public const string CUSTOMERACCT = "C";
    public const string SPECIFICACCT = "S";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "S"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Customer Account",
      "Specific Account"
    };
  }

  public class Contract_ExpirationType
  {
    public const string EXPIRING = "E";
    public const string UNLIMITED = "U";
    public const string RENEWABLE = "R";
    public readonly string[] ID_LIST = new string[3]
    {
      "E",
      "U",
      "R"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Expiring",
      "Unlimited",
      "Renewable"
    };
  }

  public class Contract_BillingPeriod
  {
    public const string WEEK = "W";
    public const string MONTH = "M";
    public const string QUARTER = "Q";
    public const string HALFYEAR = "H";
    public const string YEAR = "Y";
    public readonly string[] ID_LIST = new string[5]
    {
      "W",
      "M",
      "Q",
      "H",
      "Y"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Week",
      "Month",
      "Quarter",
      "Half a Year",
      "Year"
    };
  }

  public class SrvOrdType_RecordType
  {
    public const string SERVICE_ORDER = "SO";
    public readonly string[] ID_LIST = new string[1]{ "SO" };
    public readonly string[] TX_LIST = new string[1]
    {
      "Service Order"
    };
  }

  public class SrvOrdType_PostTo : ID.Contract_PostTo
  {
    public const string NONE = "NA";
    public const string PROJECTS = "PM";
  }

  public class Contract_PostTo
  {
    public const string ACCOUNTS_RECEIVABLE_MODULE = "AR";
    public const string SALES_ORDER_MODULE = "SO";
    public const string SALES_ORDER_INVOICE = "SI";
  }

  public class SrvOrdType_SalesAcctSource
  {
    public const string INVENTORY_ITEM = "II";
    public const string WAREHOUSE = "WH";
    public const string POSTING_CLASS = "PC";
    public const string CUSTOMER_LOCATION = "CL";
    public readonly string[] ID_LIST = new string[4]
    {
      "II",
      "WH",
      "PC",
      "CL"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Inventory Item",
      "Warehouse",
      "Posting Class",
      "Customer/Vendor Location"
    };
  }

  public class Contract_SalesAcctSource
  {
    public const string INVENTORY_ITEM = "II";
    public const string POSTING_CLASS = "PC";
    public const string CUSTOMER_LOCATION = "CL";
    public readonly string[] ID_LIST = new string[3]
    {
      "II",
      "PC",
      "CL"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Inventory Item",
      "Posting Class",
      "Customer/Vendor Location"
    };
  }

  public class SrvOrdType_GenerateInvoiceBy
  {
    public const string CRM_AR = "CRAR";
    public const string SALES_ORDER = "SORD";
    public const string PROJECT = "PROJ";
    public const string NOT_BILLABLE = "NBIL";
    public readonly string[] ID_LIST = new string[4]
    {
      "CRAR",
      "SORD",
      "PROJ",
      "NBIL"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "CRM/AR",
      "Sales Order",
      "Project",
      "Not Billable"
    };
  }

  public class SrvOrdType_BillingType
  {
    public const string COST_AS_COST = "CC";
    public const string COST_AS_REVENUE = "CR";
    public readonly string[] ID_LIST = new string[2]
    {
      "CC",
      "CR"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Cost as Cost",
      "Revenue as Cost"
    };
  }

  public class BusinessAcctType
  {
    public const string CUSTOMER = "C";
    public const string PROSPECT = "P";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "P"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Customer",
      "Prospect"
    };
  }

  public class Source_Info
  {
    public const string BUSINESS_ACCOUNT = "BA";
    public const string CUSTOMER_CONTACT = "CC";
    public const string BRANCH_LOCATION = "BL";
  }

  public class SrvOrdType_AppAddressSource : ID.Source_Info
  {
    public readonly string[] ID_LIST = new string[3]
    {
      "BA",
      "CC",
      "BL"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Business Account",
      "Contact",
      "Branch Location"
    };
  }

  public class SrvOrdType_AppContactInfoSource : ID.Source_Info
  {
    public readonly string[] ID_LIST = new string[2]
    {
      "BA",
      "CC"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Business Account",
      "Contact"
    };
  }

  public class ValidationType
  {
    public const string PREVENT = "D";
    public const string WARN = "W";
    public const string NOT_VALIDATE = "N";
    public readonly string[] ID_LIST = new string[3]
    {
      "N",
      "W",
      "D"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Do Not Validate",
      "Warn",
      "Prevent"
    };
  }

  public class SourcePrice
  {
    public const string CONTRACT = "C";
    public const string PRICE_LIST = "P";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "P"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Contract",
      "Regular Price"
    };
  }

  public class RecordType_ContractAction
  {
    public const string CONTRACT = "C";
    public const string SCHEDULE = "S";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "S"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Contract",
      "Schedule"
    };
  }

  public class Action_ContractAction
  {
    public const string CREATE = "N";
    public const string ACTIVATE = "A";
    public const string SUSPEND = "S";
    public const string CANCEL = "X";
    public const string EXPIRE = "E";
    public const string INACTIVATE_SCHEDULE = "I";
    public const string DELETE_SCHEDULE = "D";
    public const string Copied = "C";
    public const string Renew = "R";
    public readonly string[] ID_LIST = new string[9]
    {
      "N",
      "A",
      "S",
      "X",
      "E",
      "I",
      "D",
      "C",
      "R"
    };
    public readonly string[] TX_LIST = new string[9]
    {
      "Create (New)",
      "Activate",
      "Suspend",
      "Cancel",
      "Expire",
      "Inactivate - Schedule",
      "Delete - Schedule",
      nameof (Copied),
      nameof (Renew)
    };
  }

  public class WarrantyDurationType
  {
    public const string DAY = "D";
    public const string MONTH = "M";
    public const string YEAR = "Y";
    public readonly string[] ID_LIST = new string[3]
    {
      "D",
      "M",
      "Y"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Days",
      "Months",
      "Years"
    };
  }

  public class WarrantyApplicationOrder
  {
    public const string COMPANY = "C";
    public const string VENDOR = "V";
    public readonly string[] ID_LIST = new string[2]
    {
      "C",
      "V"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Company",
      "Vendor"
    };
  }

  public class ModelType
  {
    public const string EQUIPMENT = "EQ";
    public const string REPLACEMENT = "RP";
    public readonly string[] ID_LIST = new string[2]
    {
      "EQ",
      "RP"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Equipment",
      "Replacement Part"
    };
  }

  public class SourceType_Equipment
  {
    public const string SM_EQUIPMENT = "SME";
    public const string VEHICLE = "VEH";
    public const string EP_EQUIPMENT = "EPE";
    public const string AR_INVOICE = "ARI";
    public readonly string[] ID_LIST = new string[4]
    {
      "SME",
      "VEH",
      "EPE",
      "ARI"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "SD - Equipment",
      "SD - Vehicle",
      "EP - Equipment",
      "AR - Invoice"
    };
  }

  public class SourceType_Equipment_ALL : ID.SourceType_Equipment
  {
    public const string ALL = "ALL";
    public new readonly string[] ID_LIST = new string[2]
    {
      "SME",
      nameof (ALL)
    };
    public new readonly string[] TX_LIST = new string[2]
    {
      "SD - Equipment",
      "All"
    };
  }

  public class ScreenID
  {
    public const string WRKPROCESS = "FS200000";
    public const string SERVICE_ORDER = "FS300100";
    public const string APPOINTMENT = "FS300200";
    public const string MULTI_EMPLOYEE_CALENDAR = "FS300300";
    public const string EMPLOYEE_SCHEDULE = "FS202200";
    public const string SALES_ORDER = "SO301000";
    public const string SO_INVOICE = "SO303000";
    public const string GENERATE_SERVICE_CONTRACT_APPOINTMENT = "FS500200";
    public const string BRANCH_LOCATION = "FS202500";
    public const string ROUTE_CLOSING = "FS304010";
    public const string WEB_METHOD = "FS300000";
    public const string INVOICE_BY_APPOINTMENT = "FS500100";
    public const string INVOICE_BY_SERVICE_ORDER = "FS500600";
    public const string ROUTE_DOCUMENT_DETAIL = "FS304000";
    public const string APPOINTMENT_INQUIRY = "FS400100";
    public const string SERVICE_CONTRACT = "FS305800";
    public const string CLONE_APPOINTMENT = "FS500201";
    public const string RUN_SERVICE_CONTRACT_BILLING = "FS501300";
    public const string APPOINTMENT_DETAILS = "FS400500";
    public const string ExpenseReceipt = "EP301020";
    public const string RouteServiceContract = "FS300800";
    public const string ExpenseClaim = "EP301000";
    public const string APInvoice = "AP301000";
  }

  public class FSEntityType
  {
    public const string ServiceOrder = "PX.Objects.FS.FSServiceOrder";
    public const string Appointment = "PX.Objects.FS.FSAppointment";
  }

  public class ReportID
  {
    public const string SERVICE_ORDER = "FS641000";
    public const string APPOINTMENT = "FS642000";
    public const string SERVICE_TIME_ACTIVITY = "FS654500";
    public const string APP_IN_SERVICE_ORDER = "FS642500";
    public const string ROUTE_APP_GPS_LOCATION = "FS643000";
    public const string CONTRACT_FORECAST = "FS660000";
  }

  public class OwnerType_Equipment
  {
    public const string CUSTOMER = "TP";
    public const string OWN_COMPANY = "OW";
    public readonly string[] ID_LIST = new string[2]
    {
      "TP",
      "OW"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Customer",
      "Company"
    };
  }

  public class AcumaticaErrorNumber
  {
    public const string SAVE_BUTTON_DISABLED = "#106";
  }

  public class MapsStatusCodes
  {
    public const string OK = "OK";
    public const string CREATED = "Created";
    public const string ACCEPTED = "Accepted";
    public const string BAD_REQUEST = "Bad Request";
    public const string UNAUTHORIZED = "Unauthorized";
    public const string FORBIDDEN = "Forbidden";
    public const string NOT_FOUND = "Not Found";
    public const string TOO_MANY_REQUESTS = "Too Many Requests";
    public const string INTERNAL_SERVER_ERROR = "Internal Server Error";
    public const string SERVICE_UNAVAILABLE = "Service Unavailable";
    public const string MAX_WAYPOINTS_EXCEEDED = "MAX_WAYPOINTS_EXCEEDED";
  }

  public class MapsConsts
  {
    public const string API_PREFIX = "https://dev.virtualearth.net/REST/v1/";
    public const string XML_SCHEMA = "bingSchema";
    public const string XML_SCHEMA_TAG = "bingSchema:";
    public const string XML_SCHEMA_URI = "http://schemas.microsoft.com/search/local/ws/rest/v1";
  }

  public class ScheduleMonthlySelection
  {
    public const string DAILY = "D";
    public const string WEEKLY = "W";
  }

  public class RecordType_ServiceContract
  {
    public const string SERVICE_CONTRACT = "NRSC";
    public const string ROUTE_SERVICE_CONTRACT = "IRSC";
    public const string EMPLOYEE_SCHEDULE_CONTRACT = "EPSC";
    public readonly string[] ID_LIST = new string[3]
    {
      "NRSC",
      "IRSC",
      "EPSC"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Service Contract",
      "Route Service Contract",
      "Employee Schedule Contract"
    };
  }

  public class ScheduleGenType_ServiceContract
  {
    public const string SERVICE_ORDER = "SO";
    public const string APPOINTMENT = "AP";
    public const string NONE = "NA";
    public readonly string[] ID_LIST = new string[3]
    {
      "SO",
      "AP",
      "NA"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Service Orders",
      "Appointments",
      "None"
    };
  }

  public class ActionType_ProcessServiceContracts
  {
    public const string STATUS = "CS";
    public const string PERIOD = "CP";
    public const string RENEW = "RW";
    public const string FORECAST = "FC";
    public const string EMAILQUOTE = "EQ";
    public readonly string[] ID_LIST = new string[5]
    {
      "CS",
      "CP",
      "RW",
      "FC",
      "EQ"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Update to Upcoming Status",
      "Activate Upcoming Billing Period",
      "Renew",
      "Forecast",
      "Email Quote"
    };
  }

  public class PreAcctSource_Setup
  {
    public const string CUSTOMER_LOCATION = "CL";
    public const string INVENTORY_ITEM = "IN";
    public readonly string[] ID_LIST = new string[2]
    {
      "CL",
      "IN"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Customer Location",
      "Inventory Item"
    };
  }

  public class ContactType_ApptMail
  {
    public const string VENDOR = "X";
    public const string CUSTOMER = "U";
    public const string STAFF_MEMBERS = "F";
    public const string GEOZONE_STAFF = "G";
    public const string SALESPERSON = "L";
    public readonly string[] ID_LIST = new string[7]
    {
      "B",
      "E",
      "U",
      "F",
      "X",
      "G",
      "L"
    };
    public readonly string[] TX_LIST = new string[7]
    {
      "Billing",
      "Employee",
      "Customer",
      "Staff Members",
      "Vendor",
      "Service Area Staff",
      "Salesperson"
    };
  }

  public class ContactType_ContractMail : ID.ContactType_ApptMail
  {
    public new readonly string[] ID_LIST = new string[6]
    {
      "C",
      "B",
      "U",
      "X",
      "L",
      "E"
    };
    public new readonly string[] TX_LIST = new string[6]
    {
      "Contact",
      "Billing",
      "Customer",
      "Vendor",
      "Salesperson",
      "Employee"
    };
  }

  public class Calendar_ExceptionType
  {
    public const string AVAILABILITY = "CA";
    public const string UNAVAILABILITY = "CE";
  }

  public class AppointmentAssignment_Status
  {
    public const string DELETE_APPOINTMENT_FROM_DB = "D";
    public const string UNASSIGN_APPOINTMENT_ONLY = "U";
  }

  public class EmployeeTimeSlotLevel
  {
    public const int BASE = 0;
    public const int COMPRESS = 1;
    public readonly int[] ID_LIST = new int[2]{ 0, 1 };
    public readonly string[] TX_LIST = new string[2]
    {
      "Base",
      "Compressed"
    };
  }

  public class Service_Action_Type
  {
    public const string NO_ITEMS_RELATED = "N";
    public const string PICKED_UP_ITEMS = "P";
    public const string DELIVERED_ITEMS = "D";
    public readonly string[] ID_LIST = new string[3]
    {
      "N",
      "P",
      "D"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "N/A",
      "Pick Up Items",
      "Deliver Items"
    };
  }

  public class Appointment_Service_Action_Type : ID.Service_Action_Type
  {
    public new readonly string[] TX_LIST = new string[3]
    {
      "N/A",
      "Picked Up",
      "Delivered"
    };
  }

  public class DocumentSource
  {
    public const string INVOICE_FROM_APPOINTMENT = "AP";
    public const string INVOICE_FROM_SERVICEORDER = "SO";
    public const string INVOICE_FROM_SERVICECONTRACT = "CO";
  }

  public class Billing_By
  {
    public const string APPOINTMENT = "AP";
    public const string SERVICE_ORDER = "SO";
    public const string CONTRACT = "CO";
    public readonly string[] ID_LIST = new string[2]
    {
      "AP",
      "SO"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Appointments",
      "Service Orders"
    };
  }

  public class Billing_Cycle_Type
  {
    public const string APPOINTMENT = "AP";
    public const string SERVICE_ORDER = "SO";
    public const string TIME_FRAME = "TC";
    public const string PURCHASE_ORDER = "PO";
    public const string WORK_ORDER = "WO";
    public readonly string[] ID_LIST = new string[5]
    {
      "AP",
      "SO",
      "PO",
      "WO",
      "TC"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Appointments",
      "Service Orders",
      "Customer Order",
      "External Reference",
      "Time Frame"
    };
  }

  public class Time_Cycle_Type
  {
    public const string WEEKDAY = "WK";
    public const string DAY_OF_MONTH = "MT";
    public readonly string[] ID_LIST = new string[2]
    {
      "MT",
      "WK"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Fixed Day of Month",
      "Fixed Day of Week"
    };
  }

  public class Frequency_Type
  {
    public const string WEEKLY = "WK";
    public const string MONTHLY = "MT";
    public const string NONE = "NO";
    public readonly string[] ID_LIST = new string[3]
    {
      "WK",
      "MT",
      "NO"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Weekly",
      "Monthly",
      "None"
    };
  }

  public class Send_Invoices_To
  {
    public const string BILLING_CUSTOMER_BILL_TO = "BT";
    public const string DEFAULT_BILLING_CUSTOMER_LOCATION = "DF";
    public const string SO_BILLING_CUSTOMER_LOCATION = "LC";
    public const string SERVICE_ORDER_ADDRESS = "SO";
    public readonly string[] ID_LIST = new string[4]
    {
      "BT",
      "DF",
      "LC",
      "SO"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Billing Customer",
      "Default Billing Customer Location",
      "Specific Billing Customer Location",
      "Service Order"
    };
  }

  public class Ship_To
  {
    public const string BILLING_CUSTOMER_BILL_TO = "BT";
    public const string SO_BILLING_CUSTOMER_LOCATION = "BL";
    public const string SO_CUSTOMER_LOCATION = "LC";
    public const string SERVICE_ORDER_ADDRESS = "SO";
    public readonly string[] ID_LIST = new string[4]
    {
      "BT",
      "BL",
      "LC",
      "SO"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Billing Customer",
      "Specific Billing Customer Location",
      "Specific Customer Location",
      "Service Order"
    };
  }

  public class Default_Billing_Customer_Source
  {
    public const string SERVICE_ORDER_CUSTOMER = "SO";
    public const string DEFAULT_CUSTOMER = "DC";
    public const string SPECIFIC_CUSTOMER = "LC";
    public readonly string[] ID_LIST = new string[3]
    {
      "SO",
      "DC",
      "LC"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Service Order Customer",
      "Default Customer",
      "Specific Customer"
    };
  }

  public class Batch_PostTo : ID.Batch_PostTo_Filter
  {
    public new const string SO = "SO";
    public new const string AR_AP = "AA";
    public new const string SI = "SI";
    public new const string PM = "PM";
    public const string AR = "AR";
    public const string AP = "AP";
    public const string IN = "IN";
    public new readonly string[] ID_LIST = new string[7]
    {
      nameof (AR),
      nameof (SO),
      nameof (SI),
      nameof (AP),
      nameof (IN),
      "AA",
      nameof (PM)
    };
    public new readonly string[] TX_LIST = new string[7]
    {
      "Accounts Receivable",
      "Sales Orders",
      "SO Invoices",
      "Accounts Payable",
      "Inventory",
      "AR Documents and/or AP Bills",
      "Projects"
    };
  }

  public class Batch_PostTo_Filter
  {
    public const string AR_AP = "AA";
    public const string SO = "SO";
    public const string SI = "SI";
    public const string PM = "PM";
    public readonly string[] ID_LIST = new string[4]
    {
      "AA",
      nameof (SO),
      nameof (SI),
      nameof (PM)
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "AR Documents and/or AP Bills",
      "Sales Orders",
      "SO Invoices",
      "Projects"
    };
  }

  public class TablePostSource
  {
    public const string FSAPPOINTMENT_DET = "FSAppointmentDet";
    public const string FSSO_DET = "FSSODet";
  }

  public class PriceErrorCode
  {
    public const string OK = "OK";
    public const string UOM_INCONSISTENCY = "UOM_INCONSISTENCY";
  }

  public class AcumaticaFolderName
  {
    public const string PAGE = "Pages";
  }

  public class Module
  {
    public const string SERVICE_DISPATCH = "FS";
  }

  public class EquipmentWarrantyFrom
  {
    public const string SALES_ORDER_DATE = "SD";
    public const string INSTALLATION_DATE = "AD";
    public const string EARLIEST_BOTH_DATE = "ED";
    public const string LATEST_BOTH_DATE = "LD";
  }

  public class WarratySource
  {
    public const string COMPANY = "C";
    public const string VENDOR = "V";
  }

  public class Equipment_Item_Class
  {
    public const string PART_OTHER_INVENTORY = "OI";
    public const string MODEL_EQUIPMENT = "ME";
    public const string COMPONENT = "CT";
    public const string CONSUMABLE = "CE";
    public readonly string[] ID_LIST = new string[4]
    {
      "OI",
      "ME",
      "CT",
      "CE"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Part or Other Inventory",
      "Model Equipment",
      "Component",
      "Consumable"
    };
  }

  public class Equipment_Status
  {
    public const string ACTIVE = "A";
    public const string SUSPENDED = "S";
    public const string DISPOSED = "D";
    public readonly string[] ID_LIST = new string[3]
    {
      "A",
      "S",
      "D"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Active",
      "Suspended",
      "Disposed"
    };

    public class Equipment_StatusActive : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ID.Equipment_Status.Equipment_StatusActive>
    {
      public Equipment_StatusActive()
        : base("A")
      {
      }
    }
  }

  public class Equipment_Action_Base
  {
    public const string NONE = "NO";
    public const string SELLING_TARGET_EQUIPMENT = "ST";
    public const string REPLACING_TARGET_EQUIPMENT = "RT";
    public const string CREATING_COMPONENT = "CC";
    public const string UPGRADING_COMPONENT = "UC";
    public const string REPLACING_COMPONENT = "RC";
    public readonly string[] ID_LIST = new string[4]
    {
      "NO",
      "ST",
      "RT",
      "RC"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "N/A",
      "Selling Model Equipment",
      "Replacing Target Equipment",
      "Replacing Component"
    };
  }

  public class Equipment_Action : ID.Equipment_Action_Base
  {
    public new readonly string[] ID_LIST = new string[6]
    {
      "NO",
      "ST",
      "RT",
      "CC",
      "UC",
      "RC"
    };
    public new readonly string[] TX_LIST = new string[6]
    {
      "N/A",
      "Selling Model Equipment",
      "Replacing Target Equipment",
      "Selling Optional Component",
      "Upgrading Component",
      "Replacing Component"
    };
  }

  public class Schedule_Equipment_Action : ID.Equipment_Action_Base
  {
    public new readonly string[] ID_LIST = new string[2]
    {
      "NO",
      "ST"
    };
    public new readonly string[] TX_LIST = new string[2]
    {
      "N/A",
      "Selling Model Equipment"
    };
  }

  /// <summary>EntityType for FSAddress and FSContact tables</summary>
  public class ACEntityType
  {
    public const string MANUFACTURER = "MNFC";
    public const string BRANCH_LOCATION = "BLOC";
    public const string SERVICE_ORDER = "SROR";
    public const string APPOINTMENT = "APPT";

    public class Manufacturer : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ID.ACEntityType.Manufacturer>
    {
      public Manufacturer()
        : base("MNFC")
      {
      }
    }

    public class BranchLocation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      ID.ACEntityType.BranchLocation>
    {
      public BranchLocation()
        : base("BLOC")
      {
      }
    }

    public class ServiceOrder : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ID.ACEntityType.ServiceOrder>
    {
      public ServiceOrder()
        : base("SROR")
      {
      }
    }

    public class Appointment : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ID.ACEntityType.Appointment>
    {
      public Appointment()
        : base("APPT")
      {
      }
    }
  }

  public class ServiceOrder_Action_Filter
  {
    public const string UNDEFINED = "UD";
    public const string COMPLETE = "CO";
    public const string CANCEL = "CA";
    public const string REOPEN = "RE";
    public const string CLOSE = "CL";
    public const string UNCLOSE = "UN";
    public const string ALLOWINVOICE = "AL";
    public readonly string[] ID_LIST = new string[7]
    {
      "UD",
      "CO",
      "CA",
      "RE",
      "CL",
      "UN",
      "AL"
    };
    public readonly string[] TX_LIST = new string[7]
    {
      "<SELECT>",
      "Complete",
      "Cancel",
      "Reopen",
      "Close",
      "Unclose",
      "Allow Billing"
    };
  }

  public class TimeRange_Setup
  {
    public const string DAY = "D";
    public const string WEEK = "W";
    public const string MONTH = "M";
    public readonly string[] ID_LIST = new string[3]
    {
      "D",
      "W",
      "M"
    };
    public readonly string[] TX_LIST = new string[3]
    {
      "Day",
      "Week",
      "Month"
    };
  }

  public class TimeFilter_Setup
  {
    public const string CLEARED_FILTER = "CF";
    public const string WORKING_TIME = "WT";
    public const string WEEKDAYS = "WD";
    public const string WORKING_TIME_WEEKDAYS = "WW";
    public const string BOOKED_DAYS = "BD";
    public readonly string[] ID_LIST = new string[5]
    {
      "CF",
      "WT",
      "WD",
      "WW",
      "BD"
    };
    public readonly string[] TX_LIST = new string[5]
    {
      "Cleared Filter",
      "Working Time",
      "Weekdays",
      "Working Time and Weekdays",
      "Booked Days"
    };
  }

  public class DayResolution_Setup
  {
    public const int D13 = 13;
    public const int D14 = 14;
    public const int D15 = 15;
    public const int D16 = 16 /*0x10*/;
    public const int D17 = 17;
    public const int D18 = 18;
    public const int D19 = 19;
    public readonly int[] ID_LIST = new int[6]
    {
      14,
      15,
      16 /*0x10*/,
      17,
      18,
      19
    };
    public readonly string[] TX_LIST = new string[6]
    {
      "14",
      "15",
      "16",
      "17",
      "18",
      "19"
    };
  }

  public class WeekResolution_Setup
  {
    public const int W10 = 10;
    public const int W11 = 11;
    public const int W12 = 12;
    public const int W13 = 13;
    public const int W14 = 14;
    public const int W15 = 15;
    public const int W16 = 16 /*0x10*/;
    public const int W17 = 17;
    public const int W18 = 18;
    public const int W19 = 19;
    public readonly int[] ID_LIST = new int[8]
    {
      12,
      13,
      14,
      15,
      16 /*0x10*/,
      17,
      18,
      19
    };
    public readonly string[] TX_LIST = new string[8]
    {
      "12",
      "13",
      "14",
      "15",
      "16",
      "17",
      "18",
      "19"
    };
  }

  public class MonthResolution_Setup
  {
    public const int M06 = 6;
    public const int M07 = 7;
    public const int M08 = 8;
    public const int M09 = 9;
    public const int M10 = 10;
    public const int M11 = 11;
    public const int M12 = 12;
    public const int M13 = 13;
    public const int M14 = 14;
    public const int M15 = 15;
    public const int M16 = 16 /*0x10*/;
    public const int M17 = 17;
    public const int M18 = 18;
    public const int M19 = 19;
    public readonly int[] ID_LIST = new int[10]
    {
      10,
      11,
      12,
      13,
      14,
      15,
      16 /*0x10*/,
      17,
      18,
      19
    };
    public readonly string[] TX_LIST = new string[10]
    {
      "10",
      "11",
      "12",
      "13",
      "14",
      "15",
      "16",
      "17",
      "18",
      "19"
    };
  }

  public class Status_ROOptimization
  {
    public const string NOT_OPTIMIZED = "NO";
    public const string OPTIMIZED = "OP";
    public const string NOT_ABLE = "NA";
    public const string ADDRESS_ERROR = "AE";
    public readonly string[] ID_LIST = new string[4]
    {
      "NO",
      "OP",
      "NA",
      "AE"
    };
    public readonly string[] TX_LIST = new string[4]
    {
      "Has Not Been Optimized",
      "Optimized",
      "Could Not Be Optimized",
      "Encountered Address Error"
    };
  }

  public class Type_ROOptimization
  {
    public const string ASSIGNED_APP = "AP";
    public const string UNASSIGNED_APP = "UA";
    public readonly string[] ID_LIST = new string[2]
    {
      "AP",
      "UA"
    };
    public readonly string[] TX_LIST = new string[2]
    {
      "Assigned Appointments",
      "Unassigned Appointments"
    };
  }
}

namespace AspnetCoreMvcFull.Utils
{
  public class Constants
  {
    public const string SessionKeyResponse = "_SRQResponse";
    public const string SessionKeyFullname = "_SRQFullname";
    public const string SessionKeyEmail = "_SRQEmail";
    public const string SessionKeyContact = "_SRQContact";
    public const string SessionKeyDepartment = "_SRQDepartment";
    public const string SessionKeyRole = "_SRQRole";
    public const string SessionKeyPassword = "_SRQPassword";
    public const string SessionKeyRepeatPassword = "_SRQRepeatPassword";

    public const string H2H_BRANCHES_SHORT_LINK = "https://bit.ly/35FaeiS";
    public const string SNOOPIE = "Snoopie";
    public const string ANDROID_APP = "0";
    public const string IOS_APP = "1";
    public const string WEB_APP = "2";
    public const string E_WALLET_WEB = "3";
    public const string INITIAL_CHANGE_PIN = "0";
    public const string RESETING_PINCODE = "2";
    public const int MALE = 1;
    public const int FEMALE = 0;

    public const string ANDROID_STR = "ANDROID_PHONE";
    public const string IOS_STR = "IOS_PHONE";
    public const string WINDOWS_STR = "WINDOWS_PHONE";
    public const string EWALLET_STR = "E_WALLET_WEB";


    public const string ACCOUNT_TYPE = "CA";

    public const string AGENT_ACCOUNT = "1";
    public const string CLIENT_ACCOUNT = "2";

    public const string ACTIVE = "1";
    public const string NOT_ACTIVE = "0";
    public const string CANCELLED_st = "2";
    public const string PAID = "Paid";
    public const string AWAITTING_DELIVERY = "Awaiting Delivery";

    public const string VEHICLES_CATEGORY = "7";
    public const string KEYS_CATEGORY = "9";
    public const string LIVESTOCK_CATEGORY = "12";

    //  public const double cInpc = 0.1;//10%
    //   public const double H2HCashInCommissionRate = 0.6;//40% of the total 10%
    //   public const double CashInCommissionRate = 0.2;//20% of the total 10%
    //   public const double CashOutCommissionRate = 0.2;//20% of the total 10%
    //RBZ LIMITS 
    //   public const double MIN_FX = 10;
    //   public const double MAX_FX = 1000;
    //END RBZ LIMTS

    public const string DEFAULT_PASSWORD = "1111";
    public const int saltLengthLimit = 8;
    public const string APPROVED = "APPROVED";
    public const string CINAGENT = "CINAGENT";
    public const string CINMAIN = "CINMAIN";
    public const string COUTAGENT = "COUTAGENT";

    public const string H2H_QUICKBOOKS_PROD_LINK = "https://quickbooks.api.intuit.com";//Production Base URL:
    public const string H2H_QUICKBOOKS_SANDBOX_LINK = "https://sandbox-quickbooks.api.intuit.com";//Sandbox Base URL:
                                                                                                  //TESTS POST /v3/company/4625319964619385848/account?minorversion=55
                                                                                                  //   curl -X GET 'https://sandbox-quickbooks.api.intuit.com/v3/company/REPLACE_WITH_SANDBOX_COMPANY_ID/companyinfo/REPLACE_WITH_SANDBOX_COMPANY_ID?minorversion=12' \
                                                                                                  //-H 'accept: application/json' \
                                                                                                  //-H 'authorization:Bearer REPLACE_WITH_ACCESS_TOKEN' \
                                                                                                  //-H 'content-type: application/json'
                                                                                                  //  "quickbook": {
    public const string croneUrl = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer";
    public const string batchRequest = "https://sandbox-quickbooks.api.intuit.com/v3/company/{{{company_num}}}/batch?minorversion=55";//38


    public const string SUPER_ADMIN_PASSWORD = "cPFA7uarlWem1MrRSfR6lOeeO3WXMbsp";
    //////////////////////////////////////////////////////////
    // Quickbooks consts
    //////////////////////////////////////////////////////////
    public const string QUICKBOOK_DUPLICATE_USER_ERROR = "6240";
    public const string QUICKBOOK_CUSTOMER_DELETED_ERROR_WHILE_INVOICING = "6250";
    public const string QUICKBOOK_OBJECT_NOT_FOUND = "610";


    public static class AdminDetails
    {
      public const string ADMIN_EMAIL = "mumeraalois@gmail.com"; //"mumeraalois@gmail.com";
      public const string ADMIN_PHONE_NUMBER = "+263783211562";

      public const String API_SANDBOX_USER_ID = "abc_sandbox_appmerchant001";
      public const String API_SANDBOX_KEY_PWD = "ECyFE7Adq6ggZ6";
    }

    public static class URLs
    {
      public static string DEFAULT_HTTP_URL = "https://us-central1-uware-bdb26.cloudfunctions.net/uware_ssqueries";//google cloud express url calls
      public static string URL_CF_SEND_UWARE_SMS_URL = DEFAULT_HTTP_URL+"/api/v2/send_uware_sms";
    }

    public static class loginType
    {
      public const int CUSTOMER = 1;
      public const int AGENT = 0;
    }
    public static class AuthLevels
    {
      public const int APPROVALS = 1;
      public const int TOP_UPS = 2;
      public const int PAYOUTS = 3;
    }

    public static class quickbookObjects
    {
      public const String API_CRONJOB_KEY_PWD = "cPFA7uarlWem1MrRSfR6lOeeO3WXMbsp::ECyFE7Adq6ggZ6";
      public const int INVOICE_ID = 23;
      public const int CREDIT_MEMO_ID = 25;
      public const int MAXIMUM_BATCH_REQUESTS_PER_MINUTE = 40;
      public const int MAXIMUM_REQUESTS_PER_BATCH = 30;
    }


    public static class thirdPartyName
    {
      public const string QUICKBOOKS = "Quickbook";
      public const string ELM = "Elm";
      public const string INVOICE = "Invoice";
      public const string Bill = "Bill";
      public const string BillPayment = "BillPayment";
      public const string Payment = "Payment";
    }

    public static class thirdPartyType
    {
      public const int CUSTOMER = 0;
      public const int AGENT = 1;
      public const int INVOICE = 2;
      public const int BILL = 3;
      public const int BILLPAYMENT = 4;
      public const int PAYMENT = 5;
      public const int VEHICLE = 6;
      public const int BILL_INCOMM = 7;
      public const int BILLPAYMENT_INCOMM = 8;
      public const int BILL_OUTCOMM = 9;
      public const int BILLPAYMENT_OUTCOMM = 10;
      public const int VENDOR_CREDIT = 11;
    }
    public static class CommissionTypes
    {
      public const string WALLET_TOPUP = "WalletTopUp";
      public const string SEND_MONEY = "SendMoney";
    }

    public static class PaymentRefencesEndFix
    {
      public const string SNP = "SN"; // Snoopie
      public const string TUP = "TP"; // Top up
      public const string PAY = "PY"; // Payment
      public const string SMY = "SM"; // Send Money
      public const string ART = "AT"; // AirTime
      public const string ZES = "ZS"; // ZESA
      public const string COM = "CM"; // COMMISSION
    }

    public static class TransactionTypes
    {
      public const string SEND_MONEY = "112";
      public const string PAY_FEES = "113";
      public const string COMMISSION = "114";
    }

    public static class Actions
    {

      public const int SERVICE_REQUEST_APP_VERSION = 2010;
      public const int REGISTRATION_REQUEST = 2011;
      public const int LOGIN_REQUEST = 2012;
      public const int SEARCH_LOST_ITEM_REQUEST = 2013;
      public const int REPORT_LOST_ITEM_REQUEST = 2014;
      public const int UNLOCK_LOGIN_REQUEST = 2015;
      public const int ADD_ITEM_REQUEST = 2016;
      public const int SEARCH_REQUEST = 2017;
      public const int CCLOGIN_REQUEST = 2018;
      public const int PAY_SEARCH_FEE_REQUEST = 2019;
      public const int REPORT_MY_LOST_ITEM_REQUEST = 2020;
      public const int REPORT_FOUND_ITEM_REQUEST = 2021;
      public const int WALLET_TO_WALLET_REQUEST = 2022;
      public const int MINI_STATEMENT_REQUEST = 2023;
      public const int GET_MYITEMS_REQUEST = 2024;
      public const int LOST_AND_FOUND_ITEMS_REQUEST = 2025;
      public const int MONTHLY_ANALYTICS_REQUEST = 2026;
      public const int POLL_PAYNOW_WALLET_TOP_UP_REQUEST = 2027;
      public const int CHECK_RECENT_PAYNOW_WALLET_TOPUP_STATUS_REQUEST = 2028;
      public const int EDIT_ACCOUNT_DATA_REQUEST = 2029;
      public const int SEARCH_CCENTRE_REQUEST = 2030;
      public const int PAY_SUBS_REQUEST = 2031;
      public const int POLL_VP_WALLET_TOP_UP_REQUEST = 2032;
      public const int CHECK_RECENT_VP_WALLET_TOPUP_STATUS_REQUEST = 2033;
      public const int ITEM_FOUND_CHECK_LAST_PAYMENT_REQUEST = 2034;
      public const int GET_FOUND_ITEMS_REQUEST = 2035;
      public const int REGISTER_CLIENT_REQUEST = 2036;
      public const int GET_LOST_ITEMS_REQUEST = 2037;
      public const int SCOMMERCE_TOPUP_ACCOUNT_REQUEST = 2038;
      public const int CUSTOMER_GETACCOUNT_REQUEST = 2039;
      public const int GET_PROMOTED_LANDF_ITEMS_REQUEST = 2040;
      public const int CASHOUT_INIT_REQUEST = 2041;
      public const int CASHOUT_REQUEST = 2042;
      public const int CASH_CHECK_REQUEST = 2043;
      public const int AUTHENTICATE_ID_REQUEST = 2044;
      public const int LATEST_NEWS = 2045;
      public const int EARNINGS_HISTORY = 2046;
      public const int APP_PRICING_REQUEST = 2047;
      public const int MY_SUBSCRIPTION_STATUS_REQUEST = 2048;
      public const int MISSING_PEOPLE_REQUEST = 2049;
      public const int REPORT_MISSING_PERSON_REQUEST = 2050;
      public const int REPPORT_DELETEITEM_REQUEST = 2051;
      public const int FORGOT_PASSWORD_REQUEST = 2052;
      public const int VERIFY_OTP = 2053;
      public const int CHANGE_PASSWORD = 2054;

    }
    public static class LogFileNames
    {

      public const string FxSystemRequestsLogTrace = "SystemRequestsLogTrace";
      public const string FxEmailRequests = "EmailRequests";
      public const string FxApprovals = "Approvals";
      public const string MsPeopleApprovals = "MsPeopleApprovals";
      public const string SecurityApprovals = "SecurityApprovals";
      public const string FxAgentAccCreation = "FxAgentAccCreation";
      public const string CCentreBranchAccCreation = "CCentreBranchAccCreation";
      public const string FxQuickBooksLogs = "QuickBooksLogs";
      public const string FxQuickBooksLogTrace = "QuickBooksLogTrace";
      public const string FxErrorsLogs = "ErrorsLogs";
      public const string SMSLogTrace = "SMSLogTrace";
      public const string FxQuickBooksTraceLogs = "QuickBooksTraceLogs";
      public const string FxQuickBooksRequestsLogs = "QuickBooksRequestsLogs";
      public const string FxNotMatchingLogTrace = "NotMatchingLogTrace";
      public const string ItemsCCentreLogTrace = "ItemsCCentreLogTrace";

    }

    public static class Responses
    {

      public const string ON_SUCCESS = "000";
      public const string ON_ITEM_PROMOTED = "002";
      public const string ON_LOGIN_STG1 = "-00";
      public const string ON_ERROR = "500";
      public const string POLL_PAYMENT_ON_ALREADY_PROCESSED = "2021";
      public const string ON_FAILURE = "404";
      public const string ON_DUPLICATE_RECORD = "333";
      public const string ON_ACCOUNT_TERMINATED = "444";
      public const string ON_ACCOUNT_SUSPENDED = "555";
      public const string ON_ACCOUNT_ACTIVATED = "666";
      public const string ON_WRONG_PINCODE = "009";
      public const string ON_USER_DOES_NOT_EXIST = "010";
      public const string ON_RECORD_EXISTS = "201";
      public const string ON_PINCODE_MUST_BE_EQUAL = "011";
      public const string ON_USER_NOT_ACTIVE = "012";
      public const string ON_WRONG_OTP_CODE = "013";
      public const string ON_ACCOUNT_LOCKED = "014";
      public const string INVALID_EMAIL = "204";
      public const string ON_INSUFFICIENT_FUNDS = "015";
      public const string ON_FRIEND_PHONE_NOT_FOUND = "016";
      public const string ON_NO_ITEM_FOUND = "017";
      public const string ON_HASH_DID_NOT_MATCH = "018";
      public const string ON_NOT_YET_FOUND = "019";
      public const string ON_MINI_STATEMENT_NOT_AVAILABLE = "020";
      public const string ON_INST_BANK_DETAILS_EXISTING = "021";
      public const string ON_INSTITUTION_EXISTING = "022";
      public const string ON_SORT_CODE_NOT_FOUND = "023";
      public const string ON_MINI_LANDF_NOT_AVAILABLE = "024";
      public const string ON_NO_AGENT_EXISTING = "025";
      public const string ON_WRONG_AGENT_PHONE_NUMBER = "026";
      public const string AUTH_ON_FAILURE = "027";
      public const string ACTION_REQUIRED = "911";
      public const string ON_ITEM_ALREADY_PAID = "028";
      public const string ON_ITEM_FOUND = "029";
      public const string ON_NO_RECORDS_FOUND = "030";
      public const string ON_ITEM_COLLECTED = "031";

      //Web constants
      public const string ON_WRONG_PASSWORD = "050";
      public const string ON_PAGE_REDIRECT = "0";
      public const string AUTH_ON_SUCCESS = "+000";
      public const string ADMIN_ON_SUCCESS = "-000";


    }

    public static class StringsConstants
    {
      public const string comm_id = "comm_id";
      public const string beneficiary_information = "Beneficiary Information";
      public const string school_fees_payment = "School fees Payment";
      public const string successful_payment = "You have successfully submitted your ";
      public const string successful_payment_cont = " school fees payment, please note, it will take 24 to 48 hours for the payment transaction to be processed, use the Hand2Hand communicator to constantly check for your school fees payment status.";

      public const string new_account_balance = "Your new account balance is";
      public const string request_failed_login_session_expired = "Request Failed, Login session expired!";

      #region WEB STRING CONSTANTS
      public const string profile_details = "Profile Details";
      public const string not_specified = "Not specified";
      public const string bank_details = "Bank Details";
      public const string payment_upload = "Payment Upload";
      public const string payment_disapproval = "Payment Disapproval";
      public const string account_details = "Account Details";
      public const string agent_account = "Agent Account";
      public const string agent_cash_in = "Agent Cash In";
      public const string agent_booking = "Agent Booking";
      public const string missing_person = "Missing Person";
      public const string h2haccount_security_issue = "Hand2Hand Agent Account Security Issues";
      public const string h2htelleraccount_security_issue = "Hand2Hand Teller Account Security Issues";

      #endregion
    }

    public class InstitutionBankDetails
    {
      public string Id { get; set; }
      public string InstitutionName { get; set; }
      public string AccountName { get; set; }
      public string AccountNo { get; set; }
      public string BankName { get; set; }
      public string BankBranch { get; set; }
      public string AccountUsage { get; set; }

    }

    #region WEB CONSTANTS
    public const string PUBLIC_NEWS_CODE = "2020PUB";
    public const string PRIVATE_MESSAGE_CODE = "2020PRIV";
    public const string NOT_ACTIVE_STATUS = "0";
    public const string ACTIVE_STATUS = "1";

    public const string IS_LOGGED_IN = "0";
    public const string IS_LOGGED_OUT = "1";

    #endregion

    #region PAYOUTS WEB CONSTANTS

    public const string INIT_ROLE = "0";
    public const string AUTH_ROLE = "2";
    public const string ADMIN_ROLE = "1";
    public const string TELLER_ROLE = "-3";
    public const string PENDING = "PENDING";
    public const string PROCESSED = "PROCESSED";
    public const string FAILED = "FAILED";
    public const string DISAPPROVED = "DISAPPROVED";
    public const string CANCELLED = "CANCELLED";
    public const string AUTHORISED = "Authorised";
    public const string FOUND_STR = "FOUND";
    public const string AWAITING = "AWAITING";
    public const string COLLECTED = "COLLECTED";

    #endregion

    public class MBlostitemDetails
    {

      public long Id;

      public string FullName;

      public string IDNumber;

      public string MobileNumber;

      public string PassPortNumber;

      public string Email;

      public string added_date;

      public string Status;

      public string ItemOwnerId;

      public string ItemOwner;

      public string ItemReporterId;

      public int IsPromoted;

      public string ItemReporter;

      public int ItemType;

      public string ItemSerialNumber;

      public int SearchCount;

      public int IsAwaitingCollection;

      public string FoundDate;

    }

  }
}

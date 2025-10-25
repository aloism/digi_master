namespace AspnetCoreMvcFull.Utils
{
  public class GlobalConstants
  { // Android app Update state, should always be above apps codes if there is a new update
    public const int AndroidAppVersionUpdateState = 1;
    public const string Verification = "1";

    // Assuming this constant is used for error responses

    // Helper class to match the JSON structure from PHP
    public class JsonErrorResponse
    {
      public int Code { get; set; }
      public string Msg { get; set; }
    }

    public static List<JsonErrorResponse> JsonError(string message)
    {
      return new List<JsonErrorResponse>
        {
            new JsonErrorResponse
            {
                Code = ON_FAILURE_RES,
                Msg = message
            }
        };
    }

    //*********************************** PHP SERVER REQUESTS ******************************
    public const int PhpAppVersionUpdate = 2024;
    public const int PhpTokenPurchaseLog = 2025;
    public const int PhpUpdTokenPurchaseLog = 2026;
    public const int PhpGetLastTokenPurchase = 2027;
    public const int PhpFlagLastTokenPurchase = 2028;
    public const int PhpUpdateTkRetries = 2029;
    public const int PhpUpdateTkReversal = 2030;
    public const int PhpCheckOutVehicle = 2031;
    public const int PhpVehicleCheckInFin = 2032;
    public const int PhpUnlockAccount = 2033;
    public const int PhpVehicleOutFin = 2034;
    public const int PhpReturnCurrentCommission = 2035;
    public const int PhpReturnCommissionsHistory = 2036;
    public const int PhpReturnAllAgentClients = 2037;
    public const int PhpSubmitClientPayment = 2038;
    public const int PhpChangePincode = 2039;
    public const int PhpRefreshAccount = 2040;
    public const int PhpReturnAllPendingSales = 2041;
    public const int ApiInitPaynowPayment = 2042;
    public const int ApiQueryPaynowPayment = 2043;
    public const int PaynowReferencePayment = 2044;
    //*********************************** GLOBAL STRINGS ******************************
    public const string UwareTeller = "UwareTeller";
    public const string PleaseEnterCorrectUsername = "Please enter a correct user name";
    public const string ViewingRestrictedLoginLevel = "Please make sure if you are using the correct login page for your status";
    public const string PleaseEnterCorrectDetails = "Please enter correct details";
    public const string PleaseEnterCorrectOldPassword = "Please enter your correct old password";
    public const string PleaseEnterCorrectPassword = "Please enter your correct password";
    public const string UsernameNotSet = "Username field was not set.";
    public const string PasswordNotSet = "Password field was not set.";
    public const string PleaseEnterAValidEmail = "Please enter a valid Email";

    // admin reg constants
    public const string YourPasswordHasBeenChanged = "Your password has been changed";
    public const string SorryThatUsernameDoesNotExist = "Sorry, that username does not exist";
    public const string PleaseEnterMessageTitle = "Please enter message title";
    public const string TitleCannotBeShorterThan2OrLongerThan64Characters = "Title cannot be shorter than 2 or longer than 64 characters";
    public const string PleaseEnterMessage = "Please enter message";
    public const string MessageMustBeNotBeLongThan160Characters = "Message must be not be long than 160 characters";
    public const string MessageMustBeNotBeLessThan15Characters = "Message must be not be less than 15 characters";
    public const string NotificationNumber = "Notification number :";
    public const string WasSuccesfullySend = ", was succesfully send !";
    public const string NotificationNotSend = "Notification not send !";
    public const string UsernameDoesNotFitTheNameScheme = "Username does not fit the name scheme: only a-Z and numbers are allowed, 2 to 20 characters";
    public const string UsernameCannotBeShorterThan2OrLongerThan64Characters = "Username cannot be shorter than 2 or longer than 64 characters";
    public const string PasswordHasAMinimumLengthOf6Characters = "Password has a minimum length of 6 characters";
    public const string PasswordDidNotMatch = "Password did not the match";
    public const string EmptyPassword = "Empty Password";
    public const string EmptyUsername = "Empty Username";
    public const string YourAccountHasBeenCreatedSuccessfullyYouCanNowLogIn = "Your account has been created successfully. You can now log in.";
    public const string SorryThatUsernameIsAlreadyTaken = "Sorry, that username is already taken.";
    public const string PasswordDidAreNotTheMatch = "Password did not match";
    public const string YourEmailAddressIsNotInAValidEmailFormat = "Your email address is not in a valid email format";
    public const string EmailCannotBeLongerThan64Characters = "Email cannot be longer than 64 characters";
    public const string EmailCannotBeEmpty = "Email cannot be empty";
    public const string PasswordAndPasswordRepeatAreNotTheSame = "Password and password repeat are not the same";
    public const string PleaseEnterCorrectDeatails = "Please enter correct details";

    //======================================== REQUEST RESPONSES
    public const int ON_SUCCESS_RES = 000;
    public const int OnSuccessResMain = 2019;
    public const int ON_FAILURE_RES = 400;
    public const int ON_NOT_FOUND_RES = 21;
    public const int OnWrongPasswordRes = 22;
    public const int OnPhoneNumberInuseRes = 23;
    public const int OnAccountNotActiveRes = 24;
    public const int OnVehicleNotFound = 25;
    public const int OnTicketNotFound = 26;
    public const int OnRequestNotAllowed = 27;
    public const int OnPaymentAlreadyConfimed = 28;
    public const int OnPaymentNotOk = 29;
    public const int OnDuplicateAcc = 30;
    public const int ON_ACC_NOT_AUTH = 31;
    public const int ON_TOO_MANY_DEVICES = 32;
    public const int OnNoDevice = 33;
    public const int ON_WRONG_OTP = 34;
    public const int OnAccReAuth = 35;
    public const int OnFloatNotMatching = 36;
    public const int OnFloatIssues = 37;
    public const int OnDubplicateReq = 38;
    public const int OnInsufficientBalanceReq = 39;
    public const int OnOldCbTrans = 40;
    public const int OnWrongCurrencyTrans = 41;
    public const int ON_WRONG_PINCODE = 108;

    //======================================== GLOBAL STRINGS
    // MESSAGE LEVELS
    public const int PubMsg = 1;
    public const int PrivMsg = 0;

    // log in levels
    public const int AdminUserLevel = 1;
    public const int AdminExpUserLevel = 2;
    public const int InfodeskUserLevel = 22;
    public const int HelpDeskUserLevel = 3;
    public const int DtClientUserLevel = 4;

    public const int TwoDecimalPlace = 2;
    public const int PolicyNumberLength = 13;
    public const int IdNumberLength = 10;
    public const int PhoneNumberLength = 13;
    public const int PasswordMinLength = 4;
    public const int PasswordMaxLength = 10;
    public const int UsernameMaxLength = 20;

    public const string ImagesRootPath = "storage/app/";
    public const string DataSaveTempFiles = "./data/SaveTempFiles/Storage/";
    public const string PolicyStatementsDataSaveTempFiles = "./data/SaveTempFiles/Storage/PolicyStatements/";
    public const string AdminSigninSessionName = "dtserv_admin_session";
    public const int LoggedOut = 1;
    public const int LoggedIn = 2;
    public const int NoAdmin = 1;
    public const int AdminExists = 2;
    public const int NoUser = 1;
    public const int UserExists = 2;
    public const int RestrictedLevel = 3;
    // 30 minute
    public const int LoginSessionDuration = 1800;

    public const int IntBundleZero = 0;
    public const int IntBundleOne = 1;
    public const int IntBundleTwo = 2;
    public const int IntBundleThree = 3;
    public const int IntBundleFour = 4;
    public const int Cancelled = 10;

    public const string FromEmail = "admin@uware.com";
    public const string FromName = "Webmaster";

    public const int SELF_REG_ADMIN = 201300;
    public const int REG_ADMIN = 201301;
    public const int CREATE_BRANCH = 201302;
    public const int CREATETELLER = 201303;
    public const int CreateStaff = 201304;
    public const int FloatCabin = 201305;
    public const int SecurityDiffMobile = 201306;
    public const int SECURITY_DV_2FA = 201307;
    public const int FloatTransfer = 201308;
    public const int ApproveStaff = 201309;
    public const int BlockStaff = 201310;
    public const int LOGIN_ADMIN = 201311;
    public const int FloatUpdate = 201312;
    public const int FloatDeclined = 201313;
    public const int PendingFloatApproval = 201314;
    public const int PendingFloatCancelled = 201315;
    public const int UnBlockStaff = 201316;
    public const int CabinReversal = 201317;
    public const int CabinExtrasFloat = 201318;
    public const int CabinReversalCancelled = 201319;
    public const int CabinBuynotes = 201320;
    public const int CabinBuynotesCancelled = 201321;
    public const int CabinBuypzgspecial = 201322;
    public const int CabinBuypzgspecialCancelled = 201323;
    public const int WebJournal = 201324;
    public const int UpdateTeller = 201325;
    public const int CabinExtrasOut = 201326;
    public const int UPDATE_ACCOUNT = 201327;
    public const int CREATE_T_ACCOUNT = 201328;
    public const int UPDATE_T_ACCOUNT = 201329;
    public const int ActivateReversal = 201330;
    public const int CREATE_USER = 201331;
    public const int DEVICE_REG = 201332;
    public const int CREATE_CASHIER = 201333;

    public const string FloatCabinStr = "FLOAT";
    public const string FloatCabinCbIn = "CBIN";
    public const string FloatCabinCbOut = "CBOUT";
    public const string TransJournal = "JOURNAL";
    public const string FloatCabinFt = "FLOAT_TRANSFER";
    public const string CabinExpense = "CABIN_EXPENSE";
    public const string TransInReversal = "TRANS_IN_REVERSAL";
    public const string TransOutReversal = "TRANS_OUT_REVERSAL";
    public const string ExpReversal = "EXP_REVERSAL";
    public const string BuyNotesRequest = "BUY_NOTES_REQUEST";
    public const string BuyNotesPurchase = "BUY_NOTES_PURCHASE";
    public const string BuyGspecialRequest = "BUY_GSPECIAL_REQUEST";
    public const string SellGspecialRequest = "SELL_GSPECIAL_REQUEST";
    public const string BuyGspecialPurchase = "BUY_GSPECIAL_PURCHASE";
    public const string SellGspecialCbout = "SELL_GSPECIAL_CBOUT";
    public const string AuditRequest = "AUDIT_REQUEST";

    public const string SMS_NOTIF_ONE = "263783211562";
    public const string SmsFlnNotifOne = "263783211562";
    public const string SmsFlnNotif2One = "263783211562";

  }
}

namespace AspnetCoreMvcFull.Models.db
{
  public class Settings
  {
      public int Id { get; set; }
      public string? Title { get; set; }
      public string? SiteName { get; set; }
      public string? SiteDesc { get; set; }
      public string? Email { get; set; }
      public string? SupportEmail { get; set; }
      public string? Mobile { get; set; }
      public int IsProduction { get; set; } = 0;
      public int Silo { get; set; } = 0;
      public string UsdNotesRate { get; set; } = "0";
      public string SurCharge { get; set; } = "0";
      public string InvoiceFlatFee { get; set; } = "0";
      public string PercentageBasedFee { get; set; } = "0";
      public string InvoicingType { get; set; } = "0";
      public string RbzStatus { get; set; } = "0";
      public int SubmitRbzTransStatus { get; set; } = 0;
      public int? BalanceReg { get; set; }
      public int? EmailNotify { get; set; }
      public int? SmsNotify { get; set; }
      public int? Kyc { get; set; }
      public int? TransferCharge { get; set; }
      public string? TransferChargep { get; set; }
      public string? MinTransfer { get; set; }
      public string? MerchantCharge { get; set; }
      public string? MerchantChargep { get; set; }
      public string? InvoiceCharge { get; set; }
      public string? InvoiceChargep { get; set; }
      public string? ProductCharge { get; set; }
      public string? ProductChargep { get; set; }
      public string? SingleCharge { get; set; }
      public string? SingleChargep { get; set; }
      public string? DonationCharge { get; set; }
      public string? DonationChargep { get; set; }
      public string? SubscriptionCharge { get; set; }
      public string? SubscriptionChargep { get; set; }
      public string? BillCharge { get; set; }
      public string? BillChargep { get; set; }
      public int? EmailVerification { get; set; }
      public int? SmsVerification { get; set; }
      public int? Registration { get; set; }
      public string? WithdrawCharge { get; set; }
      public string? WithdrawChargep { get; set; }
      public string? WithdrawLimit { get; set; }
      public string? StarterLimit { get; set; }
      public string? WithdrawDuration { get; set; }
      public int Merchant { get; set; }
      public int Transfer { get; set; } = 1;
      public int RequestMoney { get; set; } = 1;
      public int Invoice { get; set; } = 1;
      public int Store { get; set; } = 1;
      public int Donation { get; set; } = 1;
      public int Single { get; set; } = 1;
      public int Subscription { get; set; } = 1;
      public int? Bill { get; set; } = 1;
      public int? Vcard { get; set; }
      public string? Livechat { get; set; }
      public int Language { get; set; } = 0;
      public int Recaptcha { get; set; } = 0;
      public string? NextSettlement { get; set; }
      public string? Duration { get; set; }
      public string? Xperiod { get; set; }
      public string? Period { get; set; }
      public int? VcNo { get; set; }
      public float? VcMin { get; set; }
      public float? VcMax { get; set; }
      public string? StripeChargebacks { get; set; }
      public string? WelcomeMessage { get; set; }
      public int StripeConnect { get; set; } = 0;
      public string? LockCode { get; set; }
      public int? KycRestriction { get; set; }
      public int? CountryRestriction { get; set; }
      public DateTime CreatedAt { get; set; } = DateTime.Now;
      public DateTime UpdatedAt { get; set; } = new DateTime(2022, 12, 31, 22, 0, 0);
    
  }
}

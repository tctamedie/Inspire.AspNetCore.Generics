namespace Inspire.Security.Models
{
    public class SecurityOption: MakerChecker<int>
    {
        public int MinimumPasswordLength { get; set; }
        public int MinimumUppercaseLetters { get; set; }
        public int MinimumLowercaseLetters { get; set; }
        public int MinimumDigits { get; set; }
        public int MinimumSymbols { get; set; }
        public int PasswordExpiryDays { get; set; }

        public string EmailSender { get; set; }
        public string SenderPassword { get; set; }
        public string ExchangeServer { get; set; }
        public string SenderAddress { get; set; }
        public string Password { get; set; }
        public bool EnableADAuthentication { get; set; }
        public string DomainPath { get; set; }
        public string Domain { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankID { get; set; }
    }

    public class SecurityOptionDto : MakerCheckerDto<int>
    {
        public int MinimumPasswordLength { get; set; }
        public int MinimumUppercaseLetters { get; set; }
        public int MinimumLowercaseLetters { get; set; }
        public int MinimumDigits { get; set; }
        public int PasswordExpiryDays { get; set; }
        public int MinimumSymbols { get; set; }
        public string EmailSender { get; set; }
        public string SenderPassword { get; set; }
        public string ExchangeServer { get; set; }
        public string SenderAddress { get; set; }
        public string Password { get; set; }
        public bool EnableADAuthentication { get; set; }
        public string DomainPath { get; set; }
        public string Domain { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankID { get; set; }
    }

}

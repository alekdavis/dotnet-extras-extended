using Force.DeepCloner;

namespace DeepCopyTest
{
    public class DeepCopyTests
    {
        Microsoft.Graph.Models.User _graphUser;

        public DeepCopyTests()
        {
            _graphUser = new()
            {
                AccountEnabled      = true,
                UserPrincipalName   = "test.account@company.com",
                Surname             = "Surname",
                GivenName           = "GivenName",
                DisplayName         = "DisplayName",
                Mail                = "test.account@my.company.com",
                MailNickname        = "test.account",
                CompanyName         = "Company",
                PreferredLanguage   = "en-US",
                UserType            = "Guest",
                MobilePhone         = "123-456-7890",
                PasswordProfile     = new()
                {
                    ForceChangePasswordNextSignIn   = true,
                    Password                        = "secret"
                },
                OtherMails = [ "one@company.com", "two@company.com" ]
            };
        }

        [Fact]
        public void Test_DeepCopy()
        {
            // DeepCopy
            Microsoft.Graph.Models.User clone = DeepCopy.DeepCopier.Copy(_graphUser);
            Validate(_graphUser, clone);
        }

        [Fact]
        public void Test_FastDeepCloner()
        {
            // FastDeepCloner
            Microsoft.Graph.Models.User clone = FastDeepCloner.DeepCloner.Clone(_graphUser);
            Validate(_graphUser, clone);
        }

        [Fact]
        public void Test_DeepCloner()
        {
            // DeepCloner
            Microsoft.Graph.Models.User clone = DeepClonerExtensions.DeepClone<Microsoft.Graph.Models.User>(_graphUser);
            Validate(_graphUser, clone);
        }

        [Fact]
        public void Test_DeepCopyExpression()
        {
            // DeepCopy.Expressions
            Microsoft.Graph.Models.User clone = DeepCopy.ObjectCloner.Clone<Microsoft.Graph.Models.User>(_graphUser);
            Validate(_graphUser, clone);
        }

        private static void Validate
        (
            Microsoft.Graph.Models.User source,
            Microsoft.Graph.Models.User clone
        )
        {
            Assert.NotNull(clone);
            Assert.Equal(source.AccountEnabled, clone.AccountEnabled);
            Assert.Equal(source.UserPrincipalName, clone.UserPrincipalName);
            Assert.Equal(source.Surname, clone.Surname);
            Assert.Equal(source.GivenName, clone.GivenName);
            Assert.Equal(source.DisplayName, clone.DisplayName);
            Assert.Equal(source.Mail, clone.Mail);
            Assert.Equal(source.MailNickname, clone.MailNickname);
            Assert.Equal(source.CompanyName, clone.CompanyName);
            Assert.Equal(source.PreferredLanguage, clone.PreferredLanguage);
            Assert.Equal(source.UserType, clone.UserType);
            Assert.Equal(source.MobilePhone, clone.MobilePhone);
            Assert.Equal(source.OtherMails?.Count, clone.OtherMails?.Count);
            Assert.Equal(source.OtherMails?[0], clone.OtherMails?[0]);
            Assert.Equal(source.OtherMails?[1], clone.OtherMails?[1]);
            Assert.Equal(source.PasswordProfile?.ForceChangePasswordNextSignIn, clone.PasswordProfile?.ForceChangePasswordNextSignIn);
            Assert.Equal(source.PasswordProfile?.Password, clone.PasswordProfile?.Password);
        }
    }
}
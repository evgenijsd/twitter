namespace InterTwitter.ViewModels.Validators
{
    public static class ValidatorsExtension
    {
        static ValidatorsExtension()
        {
            CreatePageValidator = new CreatePageValidator();
            LogInPageValidator = new LogInPageValidator();
            PasswordPageValidator = new PasswordPageValidator();
        }

        public static readonly CreatePageValidator CreatePageValidator;

        public static readonly LogInPageValidator LogInPageValidator;

        public static readonly PasswordPageValidator PasswordPageValidator;
    }
}

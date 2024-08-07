using System.Text;

namespace Inspire.Security.Infrastructure;

public class SystemUserRepository(SecurityContext db,
    ISystemUserProfileRepository userProfileRepository, 
    IEncryptionService encryptionService,ISecurityParameterService securityParameterService,
    IEmailRepository emailRepository) : StandardMakerCheckerService<SystemUser, SystemUserDto, string, SecurityContext, StandardStatusFilter>(db), ISystemUserRepository
{
    SecurityOption SecurityOption;
    public string Folder { get; set; }

    protected override SystemUserDto AlignDataTransferObject(SystemUserDto row, [CallerMemberName] string caller = "")
    {
        int counter = Count<SystemUser>() + 1;
        row.Id = $"{counter:00000}";
        while (Any(s => s.Id == row.Id))
        {
            counter++;
            row.Id = $"{counter:00000}";
        }
        row.Password = encryptionService.Encrypt(row.Password);
        var securityOption = securityParameterService.Find(1);
        row.PasswordExpiryDate = DateTime.Now.AddDays(securityOption==null?89: securityOption.PasswordExpiryDays-1);
        return row;
    }
    protected override Task<OutputHandler> ManageEntityCreation(SystemUser row)
    {


        SubmitEmail(row);
        var profiles = row.Profiles.Split(",");
        List<SystemUserProfileDto> users = new List<SystemUserProfileDto>();
        foreach (var profile in profiles)
        {
            SystemUserProfileDto user = new()
            {
                UserID = row.Id,
                UserProfileID = profile,
                IsActive = true,
                UserAction = "Create"
            };
            users.Add(user);
        }
        return userProfileRepository.AddInScopeAsync(users, row.CreatedBy);

    }

    public override Task<OutputHandler> UpdateAsync(SystemUserDto row, string updatedBy, int authorisers = 0, bool captureTrail = true)
    {
        var securityOption = securityParameterService.Find(1);
        if (!string.IsNullOrEmpty(row.Password))
        {
            row.Password = encryptionService.Encrypt(row.Password);
            row.PasswordExpiryDate = DateTime.Now.AddDays(securityOption==null?89:securityOption.PasswordExpiryDays-1);

        }
        return base.UpdateAsync(row, updatedBy, authorisers,  captureTrail);
    }
    public async Task Register(RegisterModelView model, string userId)
    {

        if (string.IsNullOrEmpty(model.EmployeeCode))
            throw new ValidationException("Please enter employee code");
        if (string.IsNullOrEmpty(model.Username))
            throw new ValidationException("Please enter username");

        if (!IsValidEmail(model.EmailAddress))
            throw new ValidationException("Please enter valid email address");
        
        SystemUserDto record = new()
        {
            Id = model.EmployeeCode,
            LoginId = model.Username,
            Password = CreatePassword(15),
            EmailAddress = model.EmailAddress,
        };
        var output = await AddAsync(record, "System", 0, true);

        if (output.ErrorOccured)
        {
            if (output.Description.Contains("Login Id already Exists"))
            {
                var user = db.Set<SystemUser>().Where(s => s.LoginId == model.Username).FirstOrDefault();

            }
            throw new ValidationException(output.Description);
        }

    }
    protected override OutputHandler Validate(SystemUserDto row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = null)
    {
        if (Any(s => s.LoginId == row.LoginId && s.Id != row.Id))
            throw new ValidationException("Login Id already Exists");
        if ((string.IsNullOrEmpty(row.Name)))
            throw new ValidationException("Please Enter Full Name for the User");
        if ((string.IsNullOrEmpty(row.LoginId)))
            throw new ValidationException("Please Enter Login ID");
        if (Any(s=>s.EmailAddress==row.EmailAddress&&s.Id!=row.LoginId))
            throw new ValidationException("Login Id is not available");
        
        return new OutputHandler();
    }
    public override IQueryable<SystemUser> SearchByFilterModel(StandardStatusFilter model, IQueryable<SystemUser> data = null)
    {
        string search = model == null || string.IsNullOrEmpty(model.Search) ? "" : model.Search.ToLower();
        return base.SearchByFilterModel(model, data).Where(s => s.Id.ToLower().Contains(search) || s.Name.ToLower().Contains(search));
    }
    
    public string CreatePassword(int length)
    {
        var securityOption = db.SecurityOptions.FirstOrDefault();
        if (securityOption != null)
            length = securityOption.MinimumPasswordLength;
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+";
        StringBuilder res = new();
        Random rnd = new();
        while (0 < length--)
        {
            res.Append(valid[rnd.Next(valid.Length)]);
        }
        return res.ToString();
    }
    public OutputHandler SubmitEmail(SystemUser User)
    {
        var rdmPwd = encryptionService.Decrypt(User.Password);
        var template = File.ReadAllText(Path.Combine(Folder, "UserRegistration.html"));
        template.Replace("[APPLICANT]", User.Name);
        template.Replace("[ENVIRONMENT]", "");
        template.Replace("[USER_ID]", User.LoginId);
        template.Replace("[PASSWORD]", rdmPwd);

        var output = new OutputHandler();


        string title = string.Format("{0}:: REGISTRATION CONFIRMATION", "".ToUpper());

        EmailQueue queue = new()
        {
            To = User.EmailAddress,
            EmailBody = template,
            Recurrence = 1,
            SentCount = 0,
            Subject = title,
        };
        int mail = emailRepository.QueueEmail(queue);

        output = "Email will be sent to the supplied Address. You will now be redirected to the Login Page in 10 seconds".Formator();
        output.Data = queue;
        return output;




    }

}
using Microsoft.Extensions.Options;

namespace Services.Administration
{

    public class SecurityRepository: Config.Services.Administration.SecurityRepository, Config.Services.Administration.ISecurityRepository
    {
        
        public SecurityRepository(WorkforceContext db, IOptions<SecurityOption> appSettings, Config.Services.Administration.IEncryptionRepository encryption):base (db,appSettings, encryption)
        {
           
        }
        
        
    }
}

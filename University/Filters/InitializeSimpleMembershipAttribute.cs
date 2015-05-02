using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using University.Models;

namespace University.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        public class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(new UniversityContextInitializer());

                try
                {
                    using (var context = new UsersContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }
                  //  WebSecurity.InitializeDatabaseConnection("RemoteConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
                    InitUserDatabaseConnection();
                    if (!Roles.RoleExists("admin"))
                    {
                        Roles.CreateRole("admin");
                        if (!WebSecurity.UserExists("admin"))
                        {
                            WebSecurity.CreateUserAndAccount("admin", "admin");
                            Roles.AddUserToRole("admin", "admin");
                        }
                    }
                    if (!Roles.RoleExists("teacher"))
                    {
                        Roles.CreateRole("teacher");
                    }
                    if (!Roles.RoleExists("student"))
                    {
                        Roles.CreateRole("student");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            public static void InitUserDatabaseConnection()
            {
                if (!WebSecurity.Initialized)
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName",
                        autoCreateTables: true);
                }
            }
        }
    }
}

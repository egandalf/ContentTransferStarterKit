using Business.Ektron;
using Business.EPiServer;
using Common;
using Models;
using Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppTransferContentTree
{
    class Program
    {
        private static EkContentAPI ekAPI = new EkContentAPI();
        private static EPiContentAPI epAPI = new EPiContentAPI();
        private static EkMenuAPI menuAPI = new EkMenuAPI();

        private static int LanguageId = Common.Configuration.LanguageId;

        static void Main(string[] args)
        {
            var ekapi = new Business.Ektron.EkContentAPI();
            var ekmenuapi = new Business.Ektron.EkMenuAPI();
            var epapi = new Business.EPiServer.EPiContentAPI();
            
            //var content = ekapi.GetContent<GenericContent>(3044, 1033);
            //var cref = epapi.CreateContent<GenericContent>(content);

            var menu = ekmenuapi.GetMenuTree((long)6);
            ProcessMenuItems(menu, "3460");

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void ProcessMenuItems(MenuItem menu, string ParentReference)
        {
            if (menu.IsContent)
            {
                var ektronItem = ekAPI.GetContent<GenericContent>(menu.TargetId, LanguageId);
                var realType = Common.PropertyRequest.GetTypeByEktronDefinition(ektronItem.SourceStructureId);
                if (realType != null && !ektronItem.IsPageLayout)
                {
                    var method = GetMethod<EkContentAPI>(x => x.GetContent<GenericContent>(ektronItem.SourceId, ektronItem.LanguageId));
                    var gMethod = method.MakeGenericMethod(realType);
                    var q = gMethod.Invoke(ekAPI, new object[] { ektronItem.SourceId, ektronItem.LanguageId });
                    
                    ((GenericContent)q).ParentContentReference = ParentReference;

                    method = GetMethod<EPiContentAPI>(x => x.CreateContent<GenericContent>((GenericContent)q));
                    gMethod = method.MakeGenericMethod(realType);
                    var result = gMethod.Invoke(epAPI, new object[] { q });
                    menu.TargetReference = result as string;
                    Console.WriteLine(string.Format("Successfully imported item. Ektron ID: {0} \t EPiServer DXC ID: {1}", ektronItem.SourceId, menu.TargetReference));
                }
                else
                {
                    menu.TargetReference = CreateContainer(menu, ParentReference);
                    Console.WriteLine(string.Format("Imported item as Container. Ektron ID: {0} \t EPiServer DXC ID: {1}", ektronItem.SourceId, menu.TargetReference));
                }
            }
            else
            {
                menu.TargetReference = CreateContainer(menu, ParentReference);
                Console.WriteLine(string.Format("Imported submenu as Container. EPiServer DXC ID: {0}", menu.TargetReference));
            }

            if (menu.Children.Any())
            {
                foreach (var c in menu.Children)
                {
                    ProcessMenuItems(c, menu.TargetReference ?? ParentReference);
                }
            }
        }

        private static string CreateContainer(MenuItem menu, string ParentContentReference)
        {
            var c = new Container();
            c.Name = menu.Name;
            c.PrimaryUrl = menu.Href;
            c.LanguageId = LanguageId;
            c.IsPublished = true;
            c.IsPageLayout = false;
            c.ParentContentReference = ParentContentReference;
            var cref = epAPI.CreateContent<Container>(c);
            return cref;
        }

        private static MethodInfo GetMethod<T>(Expression<Action<T>> expr)
        {
            return ((MethodCallExpression)expr.Body).Method.GetGenericMethodDefinition();
        }
    }
}

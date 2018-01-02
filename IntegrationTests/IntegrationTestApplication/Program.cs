using IntegrationTests;
using System;

namespace IntegrationTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting integration tests");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var statusCode = new StatusCodeTests();
            TestRunner.RunTheMethod(statusCode.StatusCode_Root_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_Index_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_NonExsiting_NotFound);
            TestRunner.RunTheMethod(statusCode.StatusCode_AnotherPage_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_Document_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_CatGif_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_PandaJpg_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_Script_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_Style_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_Subfolder_Ok);
            TestRunner.RunTheMethod(statusCode.StatusCode_SubfolderIndex_Ok);

            var contentApproval = new ContentApprovals();
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Root);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Index);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Anotherpage);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_SubfolderRoot);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_SubfolderIndex);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Script);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Style);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Cat);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Panda);
            TestRunner.RunTheMethod(contentApproval.ContentApproval_Document);

            var responseHeader = new ResponseHeaderTests();
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_HtmlFile);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_Stylesheet);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_Javascript);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_PdfDocument);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_JpgImage);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ContentType_GifImage);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_Etag_HtmlFile);
            TestRunner.RunTheMethod(responseHeader.ResponseHeaders_ExpiresInOneYear_HtmlFile);

            var dynamic = new DynamicTests();
            TestRunner.RunTheMethod(dynamic.Dynamic_Add1And2_3AsText);
            TestRunner.RunTheMethod(dynamic.Dynamic_Add2And3_5AsXml);
            TestRunner.RunTheMethod(dynamic.Dynamic_JustOneParameter_InternalServerError);

            var cookie = new CookieTests();
            TestRunner.RunTheMethod(cookie.Cookie_RequestWithoutCookie_OneCookie);
            TestRunner.RunTheMethod(cookie.Cookie_RequestCounterPageWithCookie_TwoHits);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Tests ran in "+elapsedMs+ " ms");
            Console.ReadKey();
        }
    }
}

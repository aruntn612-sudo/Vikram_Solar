using Microsoft.Playwright;
using VKSLibrary.UI.Pages;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

namespace VKS_Tests.Ui_Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class UiTest : PageTest
    {
        HomePage homePage;

        [Test]
        public async Task PojVerifyTheVsvikramSolarHomePageTitle()
        {
            homePage = new HomePage(Page);
            await homePage.LoadVisWebsite();

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("VSVIKRAMSOLAR – Solar | Inverter | Smart CCTV | Water Purifiers"));

        }
        [Test]
        public async Task VerifyTheVsvikramSolarHomePageTitle()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");

            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("VSVIKRAMSOLAR – Solar | Inverter | Smart CCTV | Water Purifiers"));

        }
        [Test]
        public async Task VerifyTheAboutUsPageTitle()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "About Us" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "About Vikram Solar & Home" })).ToBeVisibleAsync();
            await Expect(Page.Locator("h1")).ToContainTextAsync("About Vikram Solar & Home Appliances");
        }

        [Test]
        public async Task VerifyTheOurServicesPageTitle()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" })).ToBeVisibleAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Smart Energy, Backup & Safety" })).ToBeVisibleAsync();
            await Expect(Page.Locator("h1")).ToContainTextAsync("Smart Energy, Backup & Safety Solutions");
        }
        [Test]
        public async Task VerifyTheAboutUsUrlLinkPage()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "About Us" }).ClickAsync();
            await Expect(Page).ToHaveURLAsync(new Regex(".*about"));
        }

        [Test]
        public async Task VerifyTheOurServicesUrlLinkPage()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" })).ToBeVisibleAsync();
            await Expect(Page).ToHaveURLAsync(new Regex(".*residential"));
        }
        [Test]
        public async Task VerifyTheContactUsUrlLinkPage()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Contact Us" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "contact" })).ToBeVisibleAsync();
            await Expect(Page).ToHaveURLAsync(new Regex(".*contact"));
        }

        [Test]
        public async Task OurServicesContentTest()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" })).ToBeVisibleAsync();
            await Expect(Page.Locator("#menu-item-32")).ToContainTextAsync("Our Services");
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Smart Energy, Backup & Safety" })).ToBeVisibleAsync();
            await Expect(Page.Locator("h1")).ToContainTextAsync("Smart Energy, Backup & Safety Solutions");
        }
        [Test]
        public async Task VerifyTheToContactMobileNumberDisplayedOnThePage()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            string mobileNumber = await Page.GetByText("CALL +91 95917 26143").TextContentAsync();
            mobileNumber = mobileNumber.Split("+").LastOrDefault(); await Expect(Page.GetByText("CALL +91 95917")).ToBeVisibleAsync();
            await Expect(Page.GetByRole(AriaRole.Strong)).ToContainTextAsync("CALL +91 95917 26143");
            Assert.That(mobileNumber, Is.EqualTo("91 95917 26143"));
        }

        [Test]
        public async Task VerifyOurServiceOptionOfSolarWaterHeating()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Our Services" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Smart Energy, Backup & Safety" })).ToBeVisibleAsync();
            await Expect(Page.Locator("h1")).ToContainTextAsync("Smart Energy, Backup & Safety Solutions");
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Inverter & Power Backup" })).ToBeVisibleAsync();
        }

        [Test]
        public async Task VerifyMessageFromTheFounder()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "About Us" }).ClickAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "About Vikram Solar & Home" })).ToBeVisibleAsync();
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Message from the founder" })).ToContainTextAsync("Message from the founder");
        }

        [Test]
        public async Task MyTestGetAQuoteOrInquiryViaWhatsApp()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in/");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Contact Us" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your full name" }).ClickAsync();
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your full name" }).FillAsync("Arun Kumat T N");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your full name" }).PressAsync("Tab");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your mobile number" }).FillAsync("9743950218");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Enter your mobile number" }).PressAsync("Tab");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "example@mail.com" }).PressAsync("Tab");
            await Page.Locator("#wa_date").FillAsync("2026-02-02");
            await Page.Locator("#wa_date").PressAsync("Tab");
            await Page.Locator("#wa_category").PressAsync("Tab");
            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Tell us about your" }).FillAsync("I need 300ltr solar water heater, please send me a quotation");
            var page1 = await Page.RunAndWaitForPopupAsync(async () =>
            {
                await Page.GetByRole(AriaRole.Button, new() { Name = "INQUIRE VIA WHATSAPP" }).ClickAsync();
            });
            await page1.GetByRole(AriaRole.Link, new() { Name = "Open app" }).ClickAsync();
            await page1.GotoAsync("https://web.whatsapp.com/");
            //await Expect(page1).ToHaveURLAsync(new Regex(".*web.whatsapp.com"));
            //await page1.GetByText("*WhatsApp:*").ClickAsync();
            //await page1.GetByText("*New Website Inquiry - Vikram").ClickAsync();
            //await page1.GetByTestId("conversation-compose-box-input").PressAsync("ArrowLeft");
            //await Expect(page1.GetByTestId("conversation-compose-box-input")).ToContainTextAsync("New Website Inquiry - Vikram Solar");
            //await Expect(page1.GetByTestId("conversation-compose-box-input")).ToContainTextAsync("Arun Kumat T N");
            //await page1.GetByTestId("conversation-panel-messages").PressAsync("ArrowDown");
            //await page1.GetByText("ic-attach-filewds-ic-sticker-").ClickAsync();
            //await page1.GetByTestId("conversation-compose-box-input").ClickAsync();
            //await Expect(page1.GetByTestId("conv-msg-3EB0AF3058284745F51609").GetByTestId("msg-container")).ToContainTextAsync("9743950218");
            //await page1.GetByTestId("conv-msg-3EB0AF3058284745F51609").ClickAsync();
            //await Expect(page1.GetByTestId("conv-msg-3EB0AF3058284745F51609").GetByTestId("msg-container")).ToContainTextAsync("2026-02-02");
            //await page1.GetByRole(AriaRole.Button, new() { Name = "Close" }).ClickAsync();
        }


        [Test]
        public async Task VerifyTheHomePageImage()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in");

            // Locate the target element/image
            var element = Page.Locator("#post-1480");

            string baselinePath = "baseline_image.png";
            string actualPath = "actual_image.png";

            // Capture current element state to a file
           // var element = Page.Locator("#post-1480");
if (!(await element.IsVisibleAsync()))
{
    Console.WriteLine("Element not visible. Page HTML: " + await Page.ContentAsync());
    throw new Exception("Element #post-1480 not found or not visible");
}
await element.ScreenshotAsync(new() { Path = actualPath });await element.ScreenshotAsync(new() { Path = actualPath });

            // If no baseline image exists yet (first run), save actual as baseline and pass
            if (!File.Exists(baselinePath))
            {
                File.Copy(actualPath, baselinePath);
                Assert.Inconclusive("Baseline image created. Re-run the test to compare.");
                return;
            }

            // Compare the actual image with the baseline image using ImageSharp
            bool imagesMatch = CompareImages(baselinePath, actualPath);

            Assert.IsTrue(imagesMatch, "Visual mismatch detected between the actual image and the baseline!");
        }

        private bool CompareImages(string imgPath1, string imgPath2)
        {
            using var image1 = Image.Load<Rgb24>(imgPath1);
            using var image2 = Image.Load<Rgb24>(imgPath2);

            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                return false; // Dimensions don't match
            }

            // Loop through pixels to find differences
            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    if (image1[x, y] != image2[x, y])
                    {
                        return false; // Pixel mismatch found
                    }
                }
            }

            return true;
        }
    
        [Test]
        public async Task VerifylogoImagePresenceAsync()
        {
            await Page.GotoAsync("https://www.vsvikramsolar.in");

            // 1. Locate the image by its alt text or a CSS selector
            var logoImage = Page.Locator("//*[@id='ast-desktop-header']//span/a/img");

            // 2. Assert that the image element is visible on the page
            await Assertions.Expect(logoImage).ToBeVisibleAsync();

            // 3. Optional: Verify specific attributes like 'src' to ensure it's the right image
            await Assertions.Expect(logoImage).ToHaveAttributeAsync("src", "https://www.vsvikramsolar.in/wp-content/uploads/2026/09/cropped-Finallogo-180x95.png");
        }
    }
}



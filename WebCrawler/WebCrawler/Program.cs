using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome; // Certifique-se de ter o driver do Chrome instalado
using System.Linq;

class Program
{
    static void Main()
    {
        // Inicialização do driver do Chrome
        IWebDriver driver = new ChromeDriver();

        // Informe o domínio alvo
        string targetDomain = "https://www.magma3.com.br";

        // Coletar e imprimir elementos das tags HTML em todas as URLs do domínio
        CollectAndPrintElementsInDomain(driver, targetDomain);

        // Fechar o navegador
        driver.Quit();
    }

    static void CollectAndPrintElementsInDomain(IWebDriver driver, string targetDomain)
    {
        HashSet<string> visitedUrls = new HashSet<string>();
        Queue<string> urlsToVisit = new Queue<string>();

        urlsToVisit.Enqueue(targetDomain);

        while (urlsToVisit.Count > 0)
        {
            string currentUrl = urlsToVisit.Dequeue();

            if (visitedUrls.Contains(currentUrl))
            {
                continue;
            }

            visitedUrls.Add(currentUrl);

            driver.Navigate().GoToUrl(currentUrl);

            Console.WriteLine($"Coletando elementos em: {currentUrl}");

            // Lista de tags HTML para buscar
            List<string> tagsToSearch = new List<string>
            {
                "h1", "h2", "h3", "h4", "h5", "h6",
                "p", "a", "img", "ul", "ol", "li",
                "table", "tr", "td", "th", "div",
                "span", "form", "input", "textarea",
                "button", "label", "select", "option"
            };

            foreach (string tagName in tagsToSearch)
            {
                CollectAndPrintElementsRecursive(driver.FindElement(By.TagName("body")), tagName);
            }

            IReadOnlyCollection<IWebElement> anchorElements = driver.FindElements(By.TagName("a"));

            foreach (IWebElement anchor in anchorElements)
            {
                string href = anchor.GetAttribute("href");

                if (!string.IsNullOrEmpty(href))
                {
                    Uri hrefUri;

                    if (Uri.TryCreate(href, UriKind.Absolute, out hrefUri))
                    {
                        string hrefWithoutWWW = string.Empty;

                        if (hrefUri.Host.StartsWith("www."))
                        {
                            hrefWithoutWWW = hrefUri.Host.StartsWith("www.") ? hrefUri.Host.Substring(4) : hrefUri.Host;
                        }
                        else
                        {
                            hrefUri.Host.wsswwssss    
                        }



                        if ((hrefUri.Scheme == "http" || hrefUri.Scheme == "https") &&
                            (hrefWithoutWWW == targetDomain || hrefUri.Host == targetDomain) &&
                            !visitedUrls.Contains(href))
                        {
                            urlsToVisit.Enqueue(href);
                        }
                    }
                }
            }
        }

        static void CollectAndPrintElementsRecursive(IWebElement parentElement, string tagName)
        {
            IReadOnlyCollection<IWebElement> elements = parentElement.FindElements(By.TagName(tagName));

            foreach (IWebElement element in elements)
            {
                if (string.IsNullOrWhiteSpace(element.Text)) { continue; };
                Console.WriteLine($"Elementos <{tagName}> encontrados:");
                string elementText = element.Text;
                Console.WriteLine(elementText);
            }

            IReadOnlyCollection<IWebElement> childElements = parentElement.FindElements(By.XPath("./*"));

            foreach (IWebElement childElement in childElements)
            {
                CollectAndPrintElementsRecursive(childElement, tagName);
            }
        }
    }
}

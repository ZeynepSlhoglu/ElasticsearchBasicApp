using HtmlAgilityPack;
using System.Xml;

namespace ElasticsearchBasicApp.Data
{
    public class CrawlService
    {
        public async Task<List<Article>> CrawlWebsiteAsync(string url)
        {
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(url);

            var newsArticles = new List<Article>();
             
            var newsRows = document.DocumentNode.SelectNodes("//div[@class='row mb-4']");

            if (newsRows == null)
            {
                Console.WriteLine("Haber öðeleri bulunamadý.");
                return newsArticles;
            }

            foreach (var row in newsRows)
            { 
                var imageLinkNode = row.SelectSingleNode(".//a[@class='img-holder wide']");
                var imageNode = imageLinkNode?.SelectSingleNode(".//img");
                var imageUrl = imageNode?.GetAttributeValue("src", "Resim yok") ?? "Resim yok";

                // Baþlýk ve açýklama bilgileri 
                var titleLinkNode = row.SelectSingleNode(".//a[@class='news-card-footer mb-lg-3']");
                var descriptionLinkNode = row.SelectSingleNode(".//a[@class='post-summary']");

                if (titleLinkNode == null || descriptionLinkNode == null)
                {
                    Console.WriteLine("Baþlýk veya açýklama a etiketi bulunamadý.");
                    continue;
                }

                var title = HtmlEntity.DeEntitize(titleLinkNode.InnerText.Trim());
                var description = HtmlEntity.DeEntitize(descriptionLinkNode.InnerText.Trim());
                var href = imageLinkNode?.GetAttributeValue("href", "Link yok");

                var newsArticle = new Article
                {
                    Title = title,
                    Description = description,
                    Url = href,
                    ImageUrl = imageUrl
                };

                newsArticles.Add(newsArticle);
            }

            return newsArticles;
        }

    }
}

using Nest;

namespace ElasticsearchBasicApp.Data
{
    public class ElasticsearchService
    {
        private readonly IElasticClient _elasticClient;

        public ElasticsearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task IndexArticlesAsync(List<Article> articles)
        {
            foreach (var article in articles)
            {
                await _elasticClient.IndexDocumentAsync(article);
            }
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            var searchResponse = await _elasticClient.SearchAsync<Article>(s => s
                .MatchAll()
                .Size(1000));  

            return searchResponse.Documents.ToList();
        }

        public async Task<bool> ClearAllArticlesAsync()
        {
            var deleteResponse = await _elasticClient.DeleteByQueryAsync<Article>(q => q.MatchAll());

            if (deleteResponse.ApiCall.HttpStatusCode == 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Article>> SearchArticlesAsync(string searchTerm)
        {
            var searchResponse = await _elasticClient.SearchAsync<Article>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            sh => sh.Wildcard(m => m
                                 .Field(f => f.Title)
                                 .Value($"*{searchTerm}*")),
                            sh => sh.Wildcard(m => m
                                 .Field(f => f.Description)
                                 .Value($"*{searchTerm}*")),
                            sh => sh.Match(m => m
                                .Field(f => f.Title)
                                .Query(searchTerm)
                                .Operator(Operator.And)),
                            sh => sh.Match(m => m
                                .Field(f => f.Description)
                                .Query(searchTerm)
                                .Operator(Operator.And)),
                            sh => sh.MatchPhrase(m => m
                                .Field(f => f.Title)
                                .Query(searchTerm)),
                            sh => sh.MatchPhrase(m => m
                                .Field(f => f.Description)
                                .Query(searchTerm)),
                            sh => sh.MatchPhrasePrefix(m => m
                                .Field(f => f.Title)
                                .Query(searchTerm)),
                            sh => sh.MatchPhrasePrefix(m => m
                                .Field(f => f.Description)
                                .Query(searchTerm))
                        )
                    )
                )
                .Size(1000));

            return searchResponse.Documents.ToList();
        } 
    }
}


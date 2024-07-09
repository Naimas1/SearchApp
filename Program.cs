using EO.WebBrowser.DOM;

namespace ImageSearchApp
{
    public partial class MainWindow : Window
    {
        private const string BingSubscriptionKey = "YOUR_BING_SEARCH_V7_SUBSCRIPTION_KEY";
        private const string BingEndpoint = "https://api.cognitive.microsoft.com";
        private const string GoogleApiKey = "YOUR_GOOGLE_API_KEY";
        private const string GoogleSearchEngineId = "YOUR_GOOGLE_SEARCH_ENGINE_ID";

        public object BingCheckBox { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string query = SearchTextBox.Text;
            ResultsListBox.Items.Clear();

            if (!string.IsNullOrEmpty(query))
            {
                var tasks = new List<Task<IEnumerable<ImageResult>>>();

                if (BingCheckBox.IsChecked == true)
                {
                    tasks.Add(PerformBingImageSearch(query));
                }

                if (GoogleCheckBox.IsChecked == true)
                {
                    tasks.Add(PerformGoogleImageSearch(query));
                }

                var results = await Task.WhenAll(tasks);

                foreach (var result in results.SelectMany(r => r))
                {
                    ResultsListBox.Items.Add(result);
                }
            }
        }

        private async Task<IEnumerable<ImageResult>> PerformBingImageSearch(string query)
        {
            var client = new ImageSearchClient(new ApiKeyServiceClientCredentials(BingSubscriptionKey))
            {
                Endpoint = BingEndpoint
            };

            var imageResults = await client.Images.SearchAsync(query: query);

            if (imageResults?.Value?.Count > 0)
            {
                return imageResults.Value.Select(image => new ImageResult
                {
                    Name = image.Name,
                    ThumbnailUrl = image.ThumbnailUrl.ContentUrl
                });
            }
            else
            {
                return new List<ImageResult> { new ImageResult { Name = "No results found on Bing." } };
            }
        }

        private async Task<IEnumerable<ImageResult>> PerformGoogleImageSearch(string query)
        {
            using (var httpClient = new HttpClient())
            {
                var requestUri = $"https://www.googleapis.com/customsearch/v1?q={query}&key={GoogleApiKey}&cx={GoogleSearchEngineId}&searchType=image";
                var response = await httpClient.GetStringAsync(requestUri);
                var json = JObject.Parse(response);
                var items = json["items"];

                if (items != null)
                {
                    return items.Select(item => new ImageResult
                    {
                        Name = (string)item["title"],
                        ThumbnailUrl = (string)item["link"]
                    }).ToList();
                }
                else
                {
                    return new List<ImageResult> { new ImageResult { Name = "No results found on Google." } };
                }
            }
        }
    }

    internal class GoogleCheckBox
    {
        public static bool IsChecked { get; internal set; }
    }

    internal class ResultsListBox
    {
        public static object Items { get; internal set; }
    }

    internal class SearchTextBox
    {
        public static string Text { get; internal set; }
    }

    public class ImageResult
    {
        public string Name { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}

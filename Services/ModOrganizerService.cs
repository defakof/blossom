namespace blossom.Services
{
    public class ModOrganizerService
    {
        private static ModOrganizerService _instance;
        private static readonly object _lock = new();

        private readonly List<string> supportedOrganizers = ["Mod Organizer 2"];
        private Dictionary<string, string> organizerPaths = new();

        private ModOrganizerService() { }

        public static ModOrganizerService Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    _instance ??= new ModOrganizerService();
                }
                return _instance;
            }
        }

        public bool IsOrganizerSupported(string organizerName)
        {
            return supportedOrganizers.Contains(organizerName);
        }

        public string GetUnsupportedMessage(string organizerName)
        {
            return $"We don't yet maintain {organizerName}. Please check back later for updates.";
        }

        public List<string> GetSupportedOrganizers()
        {
            return new List<string>(supportedOrganizers);
        }

        public void SaveModOrganizerPath(string organizerName, string path)
        {
            organizerPaths[organizerName] = path;
        }

        public string GetModOrganizerPath(string organizerName)
        {
            return organizerPaths.TryGetValue(organizerName, out var path) ? path : null;
        }
    }
}
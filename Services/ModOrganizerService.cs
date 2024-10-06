using System;
using System.Collections.Generic;

namespace blossom.Services
{
    public class ModOrganizerService
    {
        private static ModOrganizerService _instance;
        private static readonly object _lock = new object();

        private readonly List<string> supportedOrganizers = new List<string> { "Mod Organizer 2" };
        private Dictionary<string, string> organizerPaths = new Dictionary<string, string>();

        private ModOrganizerService() { }

        public static ModOrganizerService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new ModOrganizerService();
                        }
                    }
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
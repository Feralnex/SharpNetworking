using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Feralnex.Networking
{
    public static class AddressUtils
    {
        public static bool TryGetIPAddress(this string address, out IPAddress ipAddress, bool ipv4 = true)
        {
            ipAddress = default;

            try
            {
                IPAddress[] ipAddresses = Dns.GetHostAddresses(address);

                for (int index = 0; index < ipAddresses.Length; index++)
                {
                    ipAddress = ipAddresses[index];

                    if (ipv4 && ipAddress.AddressFamily == AddressFamily.InterNetwork) return true;
                    if (!ipv4 && ipAddress.AddressFamily == AddressFamily.InterNetworkV6) return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool TryGetIPAddresses(this string address, out IPAddress[] ipAddresses, bool ipv4 = true)
        {
            ipAddresses = default;

            try
            {
                List<IPAddress> resolvedAddresses = Dns.GetHostAddresses(address).ToList();
                List<IPAddress> matchingAddresses = new List<IPAddress>();

                foreach (IPAddress ipAddress in resolvedAddresses)
                {
                    if (ipv4 && ipAddress.AddressFamily == AddressFamily.InterNetwork) matchingAddresses.Add(ipAddress);
                    if (!ipv4 && ipAddress.AddressFamily == AddressFamily.InterNetworkV6) matchingAddresses.Add(ipAddress);
                }

                ipAddresses = matchingAddresses.ToArray();

                if (ipAddresses.Length != 0) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

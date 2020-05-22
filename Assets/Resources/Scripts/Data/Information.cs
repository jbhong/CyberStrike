public static class Information
{
    private static string[] easy1 = { "Malware was used to hack into the stations computer systems without the knowledge of any police officers:1",
                                      "Examples of malware are adware, worms, spyware and viruses etc:2",
                                      "Viruses can be planted in email/text attachments from unrecognized users or misleading websites:3",
                                      "Avoid opening suspicious email attachments or websites and update your antivirus to prevent viruses from attacking your devices:4"};

    private static string[] easy2 = { " Phishing is used to steal individuals confidential information e.g. passwords and credit card information:1",
                                      "Phishing is achieved through link manipulation, website forgery, infected attachments and malicious pop-up boxes:2",
                                      "Phishing can be reduced through multifactor authentication (MFA) or two factor authentication (2FA):3",
                                      "Authentication is used to verify the identity of users:4",
                                      "Two factor authentication (2FA) combines something you know (password & username) with something you possess (smartphone):5",
                                      "Multi-factor authentication (MFA) involves combining three authentication factors something you know (username/password), something you possess (phone) and something you inherit (fingerprint/retina scan):6"};

    private static string[] easy3 = { "Steganography is used to hide messages in a medium called covertext used to communicate between individuals:1",
                                      "Steganography involves 3 stages - encryption, embed info in covertext and output steganogram:2",
                                      "In steganography, messages can be hidden in the form of text, photos, sound files etc.:3",
                                      "Cross-site scripting (XSS) is an injection attack that is implemented by using a web application to send malicious code:4",
                                      "One type of XSS is reflected XSS - malicious script in an HTTP request:5",
                                      "Cross-site scripting is used to access personal data and capture login credentials:6",
                                      "Intrusion detection software (IDS) is a software application to monitor network traffic:7",
                                      "Two intrusion detection software (IDS) categories are host-based and network-based, which differs by the location of the sensors:8"};

    private static string[] medium1 = { "Virus, one type of malware is spread through infected email attachments and file sharing:1",
                                        "Worms, one type of malware, is a standalone software which is spread through social engineering:2",
                                        "Malware can be combated through backing up data often, advanced analytics and cloud security platforms:3",
                                        "One distribution channel of malware was is self-propagation which applies to viruses, where the virus can spread from computer to computer through infected file executables:4"};

    private static string[] medium2 = { "Phishing is used to access and steal confidential police files and reports. This is done through link manipulation – emails presenting links that spoof legitimate URLS:1",
                                        "Phishing can be achieved through website forgery – JavaScript commands that make harmful website URLs look legitimate:2",
                                        "Email filters prevents phishing through machine learning that uses DMARC protocol to prevent email spoofing:3",
                                        "Phishing emails can be detected through spelling mistakes, unusual senders or unexpected .exe attachments:4",
                                        "Phishing can be prevented through two factor authentication (2FA). This incorporates two methods of identity confirmation - something you know and something you own:5",
                                        "Multi-Factor Authentication (MFA) requires 3 authentication factors - something that the user possesses, something known and inherent factors. This is more advanced than 2FA as it implements further security authentication factors.:6"};

    private static string[] medium3 = { "Steganography is used to hide messages, with the most common steganography being embedded images :1",
                                        "Some steganography algorithms include - LSB, DCT, and Append types :2",
                                        "Steganography algorithm 1 - LSB (Least Significant Bit), embeds data in the photo by replacing the least significant bit in a photo :3",
                                        "Steganography algorithm 2 - DCT (Discrete Cosine Transform). Calculates the frequencies of the image and then replaces some of them :4",
                                        "Steganography algorithm 3 - Append type. It appends the data to the end of the file as padding :5",
                                        "Cross-site scripting was implemented by using a web application to send malicious code. Three types of XSS attacks are - Reflected XSS, Stored XSS, DOM-based XSS :6",
                                        "Intrusion detection software (IDS) monitors traffic on networks. IDE is used to block malicious traffic and is programmed to analyse traffic and identify patterns in that traffic that may indicate a cyberattack of various sorts :7",
                                        "There are so many types of IDS - host-based, VM-based, stack-based which differs based on the location of the sensors, ex. network-based IDE implements the sensors on the network :8"};

    private static string[] hard1 = { "Spyware is a form of malware that infiltrates computing devices, steals internet usage data and sensitive information. :1",
                                      "Trojans are a form of spyware that use legitimate software as a disguise to attack. It allows access to devices remotely. :2",
                                      "Adware is a form of spyware that is embedded in advertisements and tracks browsing history and downloads. :3",
                                      "System monitor spyware is disguised as freeware and records keystrokes, emails, chat-room dialogs, websites visited, and programs that are run. :4"};

    private static string[] hard2 = { "Spear phishing targets specific individuals, businesses and organizations. It requires research on the hackers part to make the emails less generic and more legitimate. :1",
                                      "Clone phishing creates nearly identical versions of emails previously received. The only difference is the attachment or link in the message that contains something malicious. :2",
                                      "Whale phishing  targets the higher ups like chief executive officers, chief operating officers, and other high-ranking executives and tricks them into giving sensitive company data. :3",
                                      "Pop-up phishing uses pop-up ads to trick people into installing malware on their computers. :4",
                                      "HTTP (Hypertext Transfer Protocol) is a world wide web protocol used to format and transfer messages. It is unsafe as messages are sent in plaintext.:5",
                                      "HTTPS (Hypertext Transfer Protocol Secure) should be used instead of HTTP to encrypt messages. It adds a Secure Sockets Layer to the HTTP protocol and establishes an encrypted link to protect data transferred between the server and client. :6"};

    private static string[] hard3 = { "LSB steganography is a technique used to hide messages inside an image. The least significant bit for each colour in an images colour coding is changed which changes the data being sent.:1",
                                      "Text steganography is a method used to hide messages inside other text. Ways that it can be done include - changing the number of tabs, white spaces or capital letters in a sentence.:2",
                                      "Steganography can be combined with cryptography to add a layer of complexity to a problem because you have to decode a message after detecting it.:3",
                                      "The three steps to a good Intrusion Detection System (IDS)are - 1st - Monitor (analyse hosts/networks), 2nd - Identify (any misuse/abnormal activities) and 3rd - Asses (the severity of the situation and alert administrators).:4",
                                      "Anomaly-based IDS monitors system activity and identifies any deviation from normal usage patterns. It applies statistical measures to classify activities/behaviours. :5",
                                      "Misuse-based IDS is signature based which represents patterns of well-known attacks that are stored in the database to filter suspicious activity. It is slower than others but more accurate.:6",
                                      "Cryptography is a method of applying algorithms to data so that it can be communicated across securely to the receiver. It prevents third parties from reading and understanding it. :7",
                                      "Intrusion Detection Systems (IDS) can be placed in host systems (HIDS) or networks (NIDS). :8"};

    public static string[] GetLevelData(string _levelpath)
    {
        if (_levelpath.Contains("E1"))
        {
            return easy1;
        }
        else if (_levelpath.Contains("E2"))
        {
            return easy2;
        }
        else if (_levelpath.Contains("E3"))
        {
            return easy3;
        }
        else if (_levelpath.Contains("M1"))
        {
            return medium1;
        }
        else if (_levelpath.Contains("M2"))
        {
            return medium2;
        }
        else if (_levelpath.Contains("M3"))
        {
            return medium3;
        }
        else if (_levelpath.Contains("H1"))
        {
            return hard1;
        }
        else if (_levelpath.Contains("H2"))
        {
            return hard2;
        }
        else
        {
            return hard3;
        }
    }
}

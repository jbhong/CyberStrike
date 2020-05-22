public class Question
{
    public string QuestionString { get; set; }
    public string Option1 { get; set; }
    public string Option2 { get; set; }
    public string Option3 { get; set; }
    public int Correct { get; set; }
}

public static class Questions
{
    private static string[] easy1 = { "1:What is the correct definition of malware?:Malicious code that causes damage without the awareness of the user:Malicious code that causes damage with the awareness of the user:Malicious code that causes harm to the user:1",
                                      "2:What are the different types of malware?:Virus, Hex, Trojan:Worm, Spyware, Adware:IPsec, Virus, Worm:2",
                                      "3:Viruses are planted through which method?:Opening a text message:Opening email:Opening an email attachment:3",
                                      "4:To reduce the risk of malware you can :Avoid opening messages:Update your antivirus:Avoid opening your computer:2"};

    private static string[] easy2 = { "1:What is the process of authentication?:Verifying identity:Accessing the system:Verifying the data you can access:1",
                                      "2:Phishing is used to?:Steal usernames and passwords through trial and error:Steal sensitive information:Identity theft:2",
                                      "3:Authentication through MFA includes which three factors?:Something you know, have and location:Something you know, own and possess:Something you know, have and inherit:3",
                                      "4:Examples of 3 different authentication factors for MFA are? A. Password, phone, retina scan  B. Username, password, email C. Password, phone, fingerprint:A,B:A,C:All the above:2",
                                      "5:Phishing is achieved through which method?:Spying:Link manipulation:Malware:2",
                                      "6:The fewer the authentication factors, the greater the security?:True:False::2"};


    private static string[] easy3 = { "1:Steganography can be implemented through the following - A. Passwords, photos B. Sound, photos C. URL’s, photos:A:B:A,C:2",
                                      "2:The only way to implemented XSS is through Reflected XSS?:True:False::2",
                                      "3:Reflected XSS involves malicious script in an HTPSS request:True:False::2",
                                      "4:Steganography to used to hide messages in a medium called clevertext?:True:False::2",
                                      "5:IDS network-based topology is used to monitor networks for any suspicious activity by placing the sensors on a host:True:False::2",
                                      "6:The steganography process is as follows - A. Encrypt message, steganogram output B. Encrypt message, embed info in clevertext, steganogram output C. Encrypt message, embed info in clovertext, steganogram output :A:B:C:2",
                                      "7:IDS comes in two major categories:Host and Hex:Network and IPS:None of the above:3",
                                      "8:IDS is a .......... used to monitor network traffic?:Malware:Software application:Authentication form:2"};

    private static string[] medium1 = { "1:This type of standalone malware is spread through infected email attachments:Social Engineering:Viruses:Worms:3",
                                        "2:The distribution channel that involves spreading through infected file executables is:Social engineering:Self-propagation:Unsolicited emails:2",
                                        "3:Methods that fight malware attacks:Do not open any emails:Data backups:Avoid file sharing:2",
                                        "4:Viruses were spread around the police station through:File Sharing:Passwords:Advanced analytics:1"};

    private static string[] medium2 = { "1:Examples of the 3 different authentication factors that make up MFA are:Password,phone,fingerprint:Username,message,retina scan:Phone,username,password:1",
                                        "2:The DMRAC protocol is used to prevent phishing by:Preventing email spoofing:Prevent high risk emails from being opened:Flag high risk emails:1",
                                        "3:Phishing employed through website forgery employs .......commands to make URL websites look legitimate:JavaScript:JavaSplit:JavavSecure:1",
                                        "4:Opening phishing emails can be avoided by paying attention to the following:Senders in your contact list:Complex vocabulary:Unrecognizable senders:3",
                                        "5:Email spoofing is a concept that involves:Emails being sent with grammatical errors:Creation of email messages with a forged sender address:Unusual email addresses:2",
                                        "6:Example of a phishing preventative is:9FA:MFA:Avoid .evv attachments:2"};

    private static string[] medium3 = { "1:Which steganography algorithm hides messages in photos by calculating the frequencies of the image and replacing some of them?:LSA:DCT:Append type:2",
                                        "2:The steganography algorithm that involves padding is:DCT:LSB:Append type:3",
                                        "3:In the LSB algorithm, which bit is changed in order to embed the data into the photo:0110010 (last digit):0110010 (first digit):0110010 (2nd last digit):1",
                                        "4:.......... is used to monitor traffic network:Steganography:XSS:IDS:3",
                                        "5:One IDS type that employs adding sensors on an endpoint is:VM-based:Network-based:Host-based:3",
                                        "6:Which of the following is a type of XSS:Append type:Reflected:CSP:2",
                                        "7:Steganography is used to hide messages between employees by embedding these messages most commonly in:Passwords:Files:None of them:3",
                                        "8:........is used to spy on individuals browsing history to identify potential cyber-attacks:XSS:IDS:None of them:3"};

    private static string[] hard1 = { "1:Which type of spyware may appear as a Java or Flash player update upon download?:Adware:Trojan:System monitors:2",
                                      "2:Which type of spyware is often disguised as freeware?:Adware:Trojan:System monitors:3",
                                      "3:Spyware can infect your computer when you...:Accept a prompt or pop-up without reading it first and download software from a reliable source.:Pirate media such as movies, music, or games and download software from a reliable source.:Accept a prompt or pop-up without reading it first and pirate media such as movies, music, or games :3",
                                      "4:Which form of spyware is often disguised as advertisements?:Adware:Trojan:System monitors:1"};

    private static string[] hard2 = { "1:Which of these emails are NOT suspicious?:member@ebay.com:michaeljames@ebay.com:paypalonline@mailworld.com:2",
                                      "2:Websites using SSL always start with:http:https:htt:2",
                                      "3:The padlock on your URL browser indicates...:A validation that the website has SSL certification and is valid:It doesn't mean anything, all URL's have it: That it's a professional website:1",
                                      "4:HTTPS stands for...:Hypertext Transfer Protocol Safe:Hypertext Transfer Protocol Secure:Hypertext Transfer Protocol Security:2",
                                      "5:An organization can protect it's executives from whaling-phishing attacks by...:Establishing insecure financial transfer rules:Making email security training optional:Implementing multi-layer security systems:3",
                                      "6:Which type of phishing targets high-profile employees?:Pop Up Phishing:Clone phishing:Whale Phishing:3"};

    private static string[] hard3 = { "1:To retrieve data that has been encoded and encrypted, the following two methods need to be used:Steganalysis and Sandboxing:Steganalysis and cryptanalysis:Vulnerability scanning and cryptanalysis:2",
                                      "2:LSB steganography involves changing bits in an image that affect what aspect of an image?:colour:size:depth:1",
                                      "3:The second step to intrusion detection is:Assess:Monitor:Identify:3",
                                      "4:Which of the following IDS are behavior based?:Anomaly:Hybrid:Misuse:1",
                                      "5:Which IDS model detects variations (new attacks)?:Hybrid:Anomaly:Misuse:2",
                                      "6:Which tool can be used to secure information and communication between a sender and receiver?:Cryptanalysis:Cryptography:Confidentiality:2",
                                      "7:A host-based IDS does what?:Sits inside a host system and monitors the system usage:Sits in a network attached at a location:Captures and monitors packets in a network:1",
                                      "8:Which IDS model is accurate and slow compared to the others?:Anomaly:Hybrid:Misuse:3"};

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

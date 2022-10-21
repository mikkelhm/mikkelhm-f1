namespace Mikkelhm_F1.Domain;

public class WikipediaInformation
{
    public string Link { get; set; }

    public WikipediaInformation(string link = "")
    {
        Link = link;
    }
}

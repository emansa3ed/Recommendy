using System.Collections.Generic;

namespace Service.Contracts
{
    public interface ISkillOntology
    {
    
        Dictionary<string, List<string>> RelatedSkills { get; }

      
        HashSet<string> ExpandSkills(IEnumerable<string> skills);
    }
} 
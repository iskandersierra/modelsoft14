//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using ModelSoft.Framework.Collections;
//using ModelSoft.Framework.DomainObjects;
//using ModelSoft.Framework.DomainObjects.Authoring;

//namespace TestConsoleApp
//{
//    public interface IPersonalAgenda : IIdentifierSpace
//    {
//        [Content]
//        IPersonalAgendaContactTypes ContactTypes { get; set; }

//        [Content]
//        IIndexedList<string, IParty> Parties { get; }
//    }

//    public interface IPersonalAgendaContactTypes : IIdentifierSpace
//    {
//        [Container]
//        IPersonalAgenda Agenda { get; }
//        IIndexedList<string, IContactType> ContactTypes { get; }
//    }
//}

using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.PubSubEvents;

namespace DocumentArchiver.Commands
{
    public class ListDirectoryContentsEvent : PubSubEvent<string> { }

    public class UpdateDocumentEvent : PubSubEvent<string> { }

    public class CompileDocumentEvent : PubSubEvent<string> { }

    public class UpdateMessagesEvent : PubSubEvent<string> { }

    public class FinalMessageEvent : PubSubEvent<string> { }

    public class ShowErrorMessageEvent : PubSubEvent<string> { }

    public class UpdateBusyEvent : PubSubEvent<bool> { }

    public static class CommandLibrary
    {
        private static RoutedUICommand compile = new RoutedUICommand("Compile", "Compile", typeof(CommandLibrary));
        private static RoutedUICommand add = new RoutedUICommand("Add", "Add", typeof(CommandLibrary));
        private static RoutedUICommand remove = new RoutedUICommand("Remove", "Remove", typeof(CommandLibrary));

        public static RoutedUICommand Compile
        {
            get { return compile; }
        }

        public static RoutedUICommand AddListItem
        {
            get { return add; }
        }

        public static RoutedUICommand RemoveListItem
        {
            get { return remove; }
        }
    }
}
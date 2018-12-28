using Cometd.Bayeux;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cometd.cometd.common
{
    public interface IClient : JSONParserGenerator<IMutableMessage>
    {
    }


    public interface IParser<T>
    {
        //WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public <T> T parse(Reader reader, Class<T> type) throws ParseException;
        T parse<T>(StreamReader reader, T type);
    }

    public interface IGenerator
    {
        string generate(object @object);
    }

    public interface JSONContext<T>: IClient, IParser<T>, IGenerator
    {
    }

    public interface JSONParserGenerator<T> where T : IMutableMessage
    {
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public T[] parse(InputStream stream) throws ParseException;
        T[] parse(Stream stream);

        //WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public T[] parse(Reader reader) throws ParseException;
        T[] parse(StreamReader reader);

        //WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: public T[] parse(String json) throws ParseException;
        T[] parse(string json);

        string generate(T message);

        string generate(T[] messages);

        IParser<T> Parser { get; }

        IGenerator Generator { get; }
    }
}

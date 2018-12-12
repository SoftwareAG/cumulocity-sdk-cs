using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
    public interface IAbstractObjectBuilder<T>
    {
    }

    public abstract class AbstractObjectBuilder<T> : IAbstractObjectBuilder<T>
    {

        //ORIGINAL LINE: protected final Map<String, AbstractObjectBuilder<?>> builders = new LinkedHashMap<String, AbstractObjectBuilder<?>>();
        protected internal readonly LinkedHashMap<string, AbstractObjectBuilder<object>> builders =
            new LinkedHashMap<string, AbstractObjectBuilder<object>>();


        //ORIGINAL LINE: protected final Map<String, List<AbstractObjectBuilder<?>>> collectionBuilders = new LinkedHashMap<String, List<AbstractObjectBuilder<?>>>();
        protected internal readonly LinkedHashMap<string, IList<AbstractObjectBuilder<object>>> collectionBuilders =
            new LinkedHashMap<string, IList<AbstractObjectBuilder<object>>>();


        //ORIGINAL LINE: protected final Map<String, Collection<?>> collectionValues = new LinkedHashMap<String, Collection<?>>();
        protected internal readonly LinkedHashMap<string, ICollection<object>> collectionValues =
            new LinkedHashMap<string, ICollection<object>>();

        protected internal readonly LinkedHashMap<string, object> values = new LinkedHashMap<string, object>();

        private T domainObject;


        public virtual T build()
        {
            lock (this)
            {
                if (domainObject == null)
                {
                    domainObject = createDomainObject();
                    fillInValues(domainObject);
                    fillInBuilderValues(domainObject);
                    fillInCollectionValues(domainObject);
                    fillInCollectionBuilderValues(domainObject);
                }

                return domainObject;
            }
        }

        protected internal abstract T createDomainObject();


        //ORIGINAL LINE: protected Object getFieldValue(final String fieldName)
        protected internal virtual object getFieldValue(string fieldName)
        {
            return values[fieldName];
        }

        public virtual IDictionary<string, object> toMap()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            //ORIGINAL LINE: for (Map.Entry<String, AbstractObjectBuilder<?>> entry : builders.entrySet())
            foreach (var entry in builders)
                result[entry.Key] = entry.Value.build();

            //ORIGINAL LINE: for (Map.Entry<String, Collection<?>> entry : collectionValues.entrySet())
            foreach (var entry in collectionValues)
            {
                var value = createListValue(typeof(IList), entry.Value);
                result[entry.Key] = value;
            }

            //ORIGINAL LINE: for (Map.Entry<String, List<AbstractObjectBuilder<?>>> entry : collectionBuilders.entrySet())
            foreach (var entry in collectionBuilders)
            {
                var value = createBuilderListValue(typeof(IList), entry.Value);
                result[entry.Key] = value;
            }

            return result;
        }

        //ORIGINAL LINE: protected void setFieldValue(final String fieldName, final Object value)
        protected internal virtual void setFieldValue(string fieldName, object value)
        {
            values[fieldName] = value;
            builders.Remove(fieldName);
        }

        //ORIGINAL LINE: protected void setFieldValues(final AbstractObjectBuilder<?> other)
        protected internal virtual void setFieldValues(AbstractObjectBuilder<object> other)
        {

                foreach (string fieldName in other.values.Keys)
                {
                    values.Add(fieldName, other.values[fieldName]);
                }           
        }

        //ORIGINAL LINE: protected void setFieldValueBuilder(final String fieldName, final AbstractObjectBuilder<?> builder)
        protected internal virtual void setFieldValueBuilder(string fieldName, AbstractObjectBuilder<object> builder)
        {
            builders[fieldName] =   builder;
        }


        //ORIGINAL LINE: protected void addCollectionFieldValue(final String fieldName, final Object value)
        protected internal virtual void addCollectionFieldValue(string fieldName, object value)
        {
            var values = collectionValues[fieldName];
            if (values == null)
            {
                values = new List<object>();
                collectionValues.Add(fieldName, values);
            }

            values.Add(value);
        }


        //ORIGINAL LINE: protected void addCollectionFieldValueBuilder(final String fieldName, final AbstractObjectBuilder<?> builder)
        protected internal virtual void addCollectionFieldValueBuilder(string fieldName,
            AbstractObjectBuilder<object> builder)
        {
            //ORIGINAL LINE: List<AbstractObjectBuilder<?>> builderList = collectionBuilders.get(fieldName);
            var builderList = collectionBuilders[fieldName];
            if (builderList == null)
            {
                //ORIGINAL LINE: builderList = new ArrayList<AbstractObjectBuilder<?>>();
                builderList = new List<AbstractObjectBuilder<object>>();
                collectionBuilders.Add(fieldName, builderList);
            }

            builderList.Add(builder);
        }

        protected internal virtual void setCollectionFieldValueBuilders(string fieldName,
            IList<AbstractObjectBuilder<object>> groupBuilders)
        {
            collectionBuilders.Add(fieldName, groupBuilders);
        }


        //ORIGINAL LINE: protected void fillInValues(final T domainObject)
        protected internal virtual void fillInValues(T domainObject)
        {
            foreach (var entry in values)
                setFieldValue(domainObject, entry.Key, entry.Value);
        }

        //ORIGINAL LINE: protected void fillInBuilderValues(final T domainObject)
        protected internal virtual void fillInBuilderValues(T domainObject)
        {

            //ORIGINAL LINE: for (Map.Entry<String, AbstractObjectBuilder<?>> entry : builders.entrySet())
            foreach (var entry in builders)
                setFieldValue(domainObject, entry.Key, entry.Value.build());
        }

        protected internal virtual void fillInCollectionValues(T domainObject)
        {

           //ORIGINAL LINE: for (Map.Entry<String, Collection<?>> entry : collectionValues.entrySet())
            foreach (var entry in collectionValues)
            {
                //Field field = findField(domainObject.GetType(), entry.Key);
                FieldInfo field = domainObject.GetType().GetField(entry.Key);
                var value = createListValue(field.GetType(), entry.Value);
                //makeAccessible(field);
                //setField(field, domainObject, value);
            }
        }

        private ICollection<object> createListValue(Type collectionType, ICollection<object> valuesList)
        {
            var builtValues = newCollection(collectionType);
             builtValues.AddRange<object>(valuesList);
             return builtValues;
            
        }

        private void fillInCollectionBuilderValues(T domainObject)
        {

            //ORIGINAL LINE: for (Map.Entry<String, List<AbstractObjectBuilder<?>>> entry : collectionBuilders.entrySet())
            foreach (var entry in collectionBuilders)
            {
                //Field field = findField(domainObject.GetType(), entry.Key);
                FieldInfo field = domainObject.GetType().GetField(entry.Key);
                var value = createBuilderListValue(field.GetType(), entry.Value);
                //setField(field, domainObject, value);
            }
        }

        private ICollection<object> createBuilderListValue(Type collectionType, IList<AbstractObjectBuilder<object>> builderList)
        {
            var builtValues = newCollection(collectionType);

            foreach (AbstractObjectBuilder<object> builder in builderList)
                 builtValues.Add(builder.build());
            return builtValues;
        }

        private ICollection<object> newCollection(Type collectionType)
        {
            if (collectionType.IsSubclassOf(typeof(System.Collections.ICollection)))
                return new List<object>();
            //if (collectionType.IsSubclassOf(typeof(IList))) return new ArrayList();
            throw new ArgumentException("Unknown collection type: " + collectionType + "!");
        }

        private void setFieldValue(object target, string fieldName, object value)
        {
            //Field field = findField(target.GetType(), fieldName);
            //FieldInfo field = domainObject.GetType().GetField(fieldName);
            //makeAccessible(field);          
            //setField(fieldName, target, value);
            target.SetPropertyValue(fieldName,value);
        }
    }
}
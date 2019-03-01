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

        protected internal readonly LinkedHashMap<string, AbstractObjectBuilder<object>> builders =
            new LinkedHashMap<string, AbstractObjectBuilder<object>>();


        protected internal readonly LinkedHashMap<string, IList<AbstractObjectBuilder<object>>> collectionBuilders =
            new LinkedHashMap<string, IList<AbstractObjectBuilder<object>>>();


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


        protected internal virtual object getFieldValue(string fieldName)
        {
            return values[fieldName];
        }

        public virtual IDictionary<string, object> toMap()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            foreach (var entry in builders)
                result[entry.Key] = entry.Value.build();

            foreach (var entry in collectionValues)
            {
                var value = createListValue(typeof(IList), entry.Value);
                result[entry.Key] = value;
            }

            foreach (var entry in collectionBuilders)
            {
                var value = createBuilderListValue(typeof(IList), entry.Value);
                result[entry.Key] = value;
            }

            return result;
        }

        protected internal virtual void setFieldValue(string fieldName, object value)
        {
            values[fieldName] = value;
            builders.Remove(fieldName);
        }

        protected internal virtual void setFieldValues(AbstractObjectBuilder<object> other)
        {

                foreach (string fieldName in other.values.Keys)
                {
                    values.Add(fieldName, other.values[fieldName]);
                }           
        }

        protected internal virtual void setFieldValueBuilder(string fieldName, AbstractObjectBuilder<object> builder)
        {
            builders[fieldName] =   builder;
        }


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


        protected internal virtual void addCollectionFieldValueBuilder(string fieldName,
            AbstractObjectBuilder<object> builder)
        {
            var builderList = collectionBuilders[fieldName];
            if (builderList == null)
            {
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


        protected internal virtual void fillInValues(T domainObject)
        {
            foreach (var entry in values)
                setFieldValue(domainObject, entry.Key, entry.Value);
        }

        protected internal virtual void fillInBuilderValues(T domainObject)
        {

            foreach (var entry in builders)
                setFieldValue(domainObject, entry.Key, entry.Value.build());
        }

        protected internal virtual void fillInCollectionValues(T domainObject)
        {

            foreach (var entry in collectionValues)
            {
                FieldInfo field = domainObject.GetType().GetField(entry.Key);
                var value = createListValue(field.GetType(), entry.Value);
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

            foreach (var entry in collectionBuilders)
            {
                FieldInfo field = domainObject.GetType().GetField(entry.Key);
                var value = createBuilderListValue(field.GetType(), entry.Value);
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
            throw new ArgumentException("Unknown collection type: " + collectionType + "!");
        }

        private void setFieldValue(object target, string fieldName, object value)
        {
            target.SetPropertyValue(fieldName,value);
        }
    }
}
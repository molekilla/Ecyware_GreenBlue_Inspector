﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Ecyware.GreenBlue.LicenseServices.Client {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class WebStore : DataSet {
        
        private WebStoreApplicationsDataTable tableWebStoreApplications;
        
        public WebStore() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected WebStore(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["WebStoreApplications"] != null)) {
                    this.Tables.Add(new WebStoreApplicationsDataTable(ds.Tables["WebStoreApplications"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public WebStoreApplicationsDataTable WebStoreApplications {
            get {
                return this.tableWebStoreApplications;
            }
        }
        
        public override DataSet Clone() {
            WebStore cln = ((WebStore)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["WebStoreApplications"] != null)) {
                this.Tables.Add(new WebStoreApplicationsDataTable(ds.Tables["WebStoreApplications"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableWebStoreApplications = ((WebStoreApplicationsDataTable)(this.Tables["WebStoreApplications"]));
            if ((this.tableWebStoreApplications != null)) {
                this.tableWebStoreApplications.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "WebStore";
            this.Prefix = "";
            this.Namespace = "http://ecyware.com/2005/WebStore";
            this.Locale = new System.Globalization.CultureInfo("es-PA");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableWebStoreApplications = new WebStoreApplicationsDataTable();
            this.Tables.Add(this.tableWebStoreApplications);
        }
        
        private bool ShouldSerializeWebStoreApplications() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void WebStoreApplicationsRowChangeEventHandler(object sender, WebStoreApplicationsRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class WebStoreApplicationsDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnApplicationID;
            
            private DataColumn columnWebStoreApplicationID;
            
            private DataColumn columnDescription;
            
            private DataColumn columnKeywords;
            
            private DataColumn columnCreateDate;
            
            private DataColumn columnUpdateDate;
            
            private DataColumn columnPublisher;
            
            private DataColumn columnApplicationName;
            
            private DataColumn columnRating;
            
            private DataColumn columnUseWebStore;
            
            private DataColumn columnUserRatingCount;
            
            private DataColumn columnDownloads;
            
            internal WebStoreApplicationsDataTable() : 
                    base("WebStoreApplications") {
                this.InitClass();
            }
            
            internal WebStoreApplicationsDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn ApplicationIDColumn {
                get {
                    return this.columnApplicationID;
                }
            }
            
            internal DataColumn WebStoreApplicationIDColumn {
                get {
                    return this.columnWebStoreApplicationID;
                }
            }
            
            internal DataColumn DescriptionColumn {
                get {
                    return this.columnDescription;
                }
            }
            
            internal DataColumn KeywordsColumn {
                get {
                    return this.columnKeywords;
                }
            }
            
            internal DataColumn CreateDateColumn {
                get {
                    return this.columnCreateDate;
                }
            }
            
            internal DataColumn UpdateDateColumn {
                get {
                    return this.columnUpdateDate;
                }
            }
            
            internal DataColumn PublisherColumn {
                get {
                    return this.columnPublisher;
                }
            }
            
            internal DataColumn ApplicationNameColumn {
                get {
                    return this.columnApplicationName;
                }
            }
            
            internal DataColumn RatingColumn {
                get {
                    return this.columnRating;
                }
            }
            
            internal DataColumn UseWebStoreColumn {
                get {
                    return this.columnUseWebStore;
                }
            }
            
            internal DataColumn UserRatingCountColumn {
                get {
                    return this.columnUserRatingCount;
                }
            }
            
            internal DataColumn DownloadsColumn {
                get {
                    return this.columnDownloads;
                }
            }
            
            public WebStoreApplicationsRow this[int index] {
                get {
                    return ((WebStoreApplicationsRow)(this.Rows[index]));
                }
            }
            
            public event WebStoreApplicationsRowChangeEventHandler WebStoreApplicationsRowChanged;
            
            public event WebStoreApplicationsRowChangeEventHandler WebStoreApplicationsRowChanging;
            
            public event WebStoreApplicationsRowChangeEventHandler WebStoreApplicationsRowDeleted;
            
            public event WebStoreApplicationsRowChangeEventHandler WebStoreApplicationsRowDeleting;
            
            public void AddWebStoreApplicationsRow(WebStoreApplicationsRow row) {
                this.Rows.Add(row);
            }
            
            public WebStoreApplicationsRow AddWebStoreApplicationsRow(System.Guid ApplicationID, string Description, string Keywords, System.DateTime CreateDate, System.DateTime UpdateDate, string Publisher, string ApplicationName, System.Decimal Rating, string UseWebStore, int UserRatingCount, int Downloads) {
                WebStoreApplicationsRow rowWebStoreApplicationsRow = ((WebStoreApplicationsRow)(this.NewRow()));
                rowWebStoreApplicationsRow.ItemArray = new object[] {
                        ApplicationID,
                        null,
                        Description,
                        Keywords,
                        CreateDate,
                        UpdateDate,
                        Publisher,
                        ApplicationName,
                        Rating,
                        UseWebStore,
                        UserRatingCount,
                        Downloads};
                this.Rows.Add(rowWebStoreApplicationsRow);
                return rowWebStoreApplicationsRow;
            }
            
            public WebStoreApplicationsRow FindByApplicationIDWebStoreApplicationID(System.Guid ApplicationID, int WebStoreApplicationID) {
                return ((WebStoreApplicationsRow)(this.Rows.Find(new object[] {
                            ApplicationID,
                            WebStoreApplicationID})));
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                WebStoreApplicationsDataTable cln = ((WebStoreApplicationsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new WebStoreApplicationsDataTable();
            }
            
            internal void InitVars() {
                this.columnApplicationID = this.Columns["ApplicationID"];
                this.columnWebStoreApplicationID = this.Columns["WebStoreApplicationID"];
                this.columnDescription = this.Columns["Description"];
                this.columnKeywords = this.Columns["Keywords"];
                this.columnCreateDate = this.Columns["CreateDate"];
                this.columnUpdateDate = this.Columns["UpdateDate"];
                this.columnPublisher = this.Columns["Publisher"];
                this.columnApplicationName = this.Columns["ApplicationName"];
                this.columnRating = this.Columns["Rating"];
                this.columnUseWebStore = this.Columns["UseWebStore"];
                this.columnUserRatingCount = this.Columns["UserRatingCount"];
                this.columnDownloads = this.Columns["Downloads"];
            }
            
            private void InitClass() {
                this.columnApplicationID = new DataColumn("ApplicationID", typeof(System.Guid), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnApplicationID);
                this.columnWebStoreApplicationID = new DataColumn("WebStoreApplicationID", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnWebStoreApplicationID);
                this.columnDescription = new DataColumn("Description", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDescription);
                this.columnKeywords = new DataColumn("Keywords", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnKeywords);
                this.columnCreateDate = new DataColumn("CreateDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCreateDate);
                this.columnUpdateDate = new DataColumn("UpdateDate", typeof(System.DateTime), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUpdateDate);
                this.columnPublisher = new DataColumn("Publisher", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnPublisher);
                this.columnApplicationName = new DataColumn("ApplicationName", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnApplicationName);
                this.columnRating = new DataColumn("Rating", typeof(System.Decimal), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnRating);
                this.columnUseWebStore = new DataColumn("UseWebStore", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUseWebStore);
                this.columnUserRatingCount = new DataColumn("UserRatingCount", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUserRatingCount);
                this.columnDownloads = new DataColumn("Downloads", typeof(int), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnDownloads);
                this.Constraints.Add(new UniqueConstraint("DocumentKey1", new DataColumn[] {
                                this.columnApplicationID,
                                this.columnWebStoreApplicationID}, true));
                this.columnApplicationID.AllowDBNull = false;
                this.columnWebStoreApplicationID.AutoIncrement = true;
                this.columnWebStoreApplicationID.AllowDBNull = false;
                this.columnWebStoreApplicationID.ReadOnly = true;
            }
            
            public WebStoreApplicationsRow NewWebStoreApplicationsRow() {
                return ((WebStoreApplicationsRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new WebStoreApplicationsRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(WebStoreApplicationsRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.WebStoreApplicationsRowChanged != null)) {
                    this.WebStoreApplicationsRowChanged(this, new WebStoreApplicationsRowChangeEvent(((WebStoreApplicationsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.WebStoreApplicationsRowChanging != null)) {
                    this.WebStoreApplicationsRowChanging(this, new WebStoreApplicationsRowChangeEvent(((WebStoreApplicationsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.WebStoreApplicationsRowDeleted != null)) {
                    this.WebStoreApplicationsRowDeleted(this, new WebStoreApplicationsRowChangeEvent(((WebStoreApplicationsRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.WebStoreApplicationsRowDeleting != null)) {
                    this.WebStoreApplicationsRowDeleting(this, new WebStoreApplicationsRowChangeEvent(((WebStoreApplicationsRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveWebStoreApplicationsRow(WebStoreApplicationsRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class WebStoreApplicationsRow : DataRow {
            
            private WebStoreApplicationsDataTable tableWebStoreApplications;
            
            internal WebStoreApplicationsRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableWebStoreApplications = ((WebStoreApplicationsDataTable)(this.Table));
            }
            
            public System.Guid ApplicationID {
                get {
                    return ((System.Guid)(this[this.tableWebStoreApplications.ApplicationIDColumn]));
                }
                set {
                    this[this.tableWebStoreApplications.ApplicationIDColumn] = value;
                }
            }
            
            public int WebStoreApplicationID {
                get {
                    return ((int)(this[this.tableWebStoreApplications.WebStoreApplicationIDColumn]));
                }
                set {
                    this[this.tableWebStoreApplications.WebStoreApplicationIDColumn] = value;
                }
            }
            
            public string Description {
                get {
                    try {
                        return ((string)(this[this.tableWebStoreApplications.DescriptionColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.DescriptionColumn] = value;
                }
            }
            
            public string Keywords {
                get {
                    try {
                        return ((string)(this[this.tableWebStoreApplications.KeywordsColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.KeywordsColumn] = value;
                }
            }
            
            public System.DateTime CreateDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableWebStoreApplications.CreateDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.CreateDateColumn] = value;
                }
            }
            
            public System.DateTime UpdateDate {
                get {
                    try {
                        return ((System.DateTime)(this[this.tableWebStoreApplications.UpdateDateColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.UpdateDateColumn] = value;
                }
            }
            
            public string Publisher {
                get {
                    try {
                        return ((string)(this[this.tableWebStoreApplications.PublisherColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.PublisherColumn] = value;
                }
            }
            
            public string ApplicationName {
                get {
                    try {
                        return ((string)(this[this.tableWebStoreApplications.ApplicationNameColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.ApplicationNameColumn] = value;
                }
            }
            
            public System.Decimal Rating {
                get {
                    try {
                        return ((System.Decimal)(this[this.tableWebStoreApplications.RatingColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.RatingColumn] = value;
                }
            }
            
            public string UseWebStore {
                get {
                    try {
                        return ((string)(this[this.tableWebStoreApplications.UseWebStoreColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.UseWebStoreColumn] = value;
                }
            }
            
            public int UserRatingCount {
                get {
                    try {
                        return ((int)(this[this.tableWebStoreApplications.UserRatingCountColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.UserRatingCountColumn] = value;
                }
            }
            
            public int Downloads {
                get {
                    try {
                        return ((int)(this[this.tableWebStoreApplications.DownloadsColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableWebStoreApplications.DownloadsColumn] = value;
                }
            }
            
            public bool IsDescriptionNull() {
                return this.IsNull(this.tableWebStoreApplications.DescriptionColumn);
            }
            
            public void SetDescriptionNull() {
                this[this.tableWebStoreApplications.DescriptionColumn] = System.Convert.DBNull;
            }
            
            public bool IsKeywordsNull() {
                return this.IsNull(this.tableWebStoreApplications.KeywordsColumn);
            }
            
            public void SetKeywordsNull() {
                this[this.tableWebStoreApplications.KeywordsColumn] = System.Convert.DBNull;
            }
            
            public bool IsCreateDateNull() {
                return this.IsNull(this.tableWebStoreApplications.CreateDateColumn);
            }
            
            public void SetCreateDateNull() {
                this[this.tableWebStoreApplications.CreateDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsUpdateDateNull() {
                return this.IsNull(this.tableWebStoreApplications.UpdateDateColumn);
            }
            
            public void SetUpdateDateNull() {
                this[this.tableWebStoreApplications.UpdateDateColumn] = System.Convert.DBNull;
            }
            
            public bool IsPublisherNull() {
                return this.IsNull(this.tableWebStoreApplications.PublisherColumn);
            }
            
            public void SetPublisherNull() {
                this[this.tableWebStoreApplications.PublisherColumn] = System.Convert.DBNull;
            }
            
            public bool IsApplicationNameNull() {
                return this.IsNull(this.tableWebStoreApplications.ApplicationNameColumn);
            }
            
            public void SetApplicationNameNull() {
                this[this.tableWebStoreApplications.ApplicationNameColumn] = System.Convert.DBNull;
            }
            
            public bool IsRatingNull() {
                return this.IsNull(this.tableWebStoreApplications.RatingColumn);
            }
            
            public void SetRatingNull() {
                this[this.tableWebStoreApplications.RatingColumn] = System.Convert.DBNull;
            }
            
            public bool IsUseWebStoreNull() {
                return this.IsNull(this.tableWebStoreApplications.UseWebStoreColumn);
            }
            
            public void SetUseWebStoreNull() {
                this[this.tableWebStoreApplications.UseWebStoreColumn] = System.Convert.DBNull;
            }
            
            public bool IsUserRatingCountNull() {
                return this.IsNull(this.tableWebStoreApplications.UserRatingCountColumn);
            }
            
            public void SetUserRatingCountNull() {
                this[this.tableWebStoreApplications.UserRatingCountColumn] = System.Convert.DBNull;
            }
            
            public bool IsDownloadsNull() {
                return this.IsNull(this.tableWebStoreApplications.DownloadsColumn);
            }
            
            public void SetDownloadsNull() {
                this[this.tableWebStoreApplications.DownloadsColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class WebStoreApplicationsRowChangeEvent : EventArgs {
            
            private WebStoreApplicationsRow eventRow;
            
            private DataRowAction eventAction;
            
            public WebStoreApplicationsRowChangeEvent(WebStoreApplicationsRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public WebStoreApplicationsRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
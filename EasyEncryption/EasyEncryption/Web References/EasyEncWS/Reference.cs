﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace EasyEncryption.EasyEncWS {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="MainServiceSoap", Namespace="http://tempuri.org/")]
    public partial class MainService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback getPubkeyOperationCompleted;
        
        private System.Threading.SendOrPostCallback DownloadOperationCompleted;
        
        private System.Threading.SendOrPostCallback getLogsOperationCompleted;
        
        private System.Threading.SendOrPostCallback getGroupsOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback retrieveOperationCompleted;
        
        private System.Threading.SendOrPostCallback uploadFilesOperationCompleted;
        
        private System.Threading.SendOrPostCallback retrieveNotificationOperationCompleted;
        
        private System.Threading.SendOrPostCallback getIsDownloadedOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public MainService() {
            this.Url = global::EasyEncryption.Properties.Settings.Default.EasyEncryption_EasyEncWS_MainService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event getPubkeyCompletedEventHandler getPubkeyCompleted;
        
        /// <remarks/>
        public event DownloadCompletedEventHandler DownloadCompleted;
        
        /// <remarks/>
        public event getLogsCompletedEventHandler getLogsCompleted;
        
        /// <remarks/>
        public event getGroupsCompletedEventHandler getGroupsCompleted;
        
        /// <remarks/>
        public event DeleteFileCompletedEventHandler DeleteFileCompleted;
        
        /// <remarks/>
        public event retrieveCompletedEventHandler retrieveCompleted;
        
        /// <remarks/>
        public event uploadFilesCompletedEventHandler uploadFilesCompleted;
        
        /// <remarks/>
        public event retrieveNotificationCompletedEventHandler retrieveNotificationCompleted;
        
        /// <remarks/>
        public event getIsDownloadedCompletedEventHandler getIsDownloadedCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getPubkey", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getPubkey() {
            object[] results = this.Invoke("getPubkey", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getPubkeyAsync() {
            this.getPubkeyAsync(null);
        }
        
        /// <remarks/>
        public void getPubkeyAsync(object userState) {
            if ((this.getPubkeyOperationCompleted == null)) {
                this.getPubkeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetPubkeyOperationCompleted);
            }
            this.InvokeAsync("getPubkey", new object[0], this.getPubkeyOperationCompleted, userState);
        }
        
        private void OngetPubkeyOperationCompleted(object arg) {
            if ((this.getPubkeyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getPubkeyCompleted(this, new getPubkeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Download", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] Download(string user, string filename, string share, string owner) {
            object[] results = this.Invoke("Download", new object[] {
                        user,
                        filename,
                        share,
                        owner});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void DownloadAsync(string user, string filename, string share, string owner) {
            this.DownloadAsync(user, filename, share, owner, null);
        }
        
        /// <remarks/>
        public void DownloadAsync(string user, string filename, string share, string owner, object userState) {
            if ((this.DownloadOperationCompleted == null)) {
                this.DownloadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDownloadOperationCompleted);
            }
            this.InvokeAsync("Download", new object[] {
                        user,
                        filename,
                        share,
                        owner}, this.DownloadOperationCompleted, userState);
        }
        
        private void OnDownloadOperationCompleted(object arg) {
            if ((this.DownloadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DownloadCompleted(this, new DownloadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getLogs", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string getLogs(string name, string owner, string group) {
            object[] results = this.Invoke("getLogs", new object[] {
                        name,
                        owner,
                        group});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void getLogsAsync(string name, string owner, string group) {
            this.getLogsAsync(name, owner, group, null);
        }
        
        /// <remarks/>
        public void getLogsAsync(string name, string owner, string group, object userState) {
            if ((this.getLogsOperationCompleted == null)) {
                this.getLogsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetLogsOperationCompleted);
            }
            this.InvokeAsync("getLogs", new object[] {
                        name,
                        owner,
                        group}, this.getLogsOperationCompleted, userState);
        }
        
        private void OngetLogsOperationCompleted(object arg) {
            if ((this.getLogsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getLogsCompleted(this, new getLogsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getGroups", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] getGroups(string username) {
            object[] results = this.Invoke("getGroups", new object[] {
                        username});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void getGroupsAsync(string username) {
            this.getGroupsAsync(username, null);
        }
        
        /// <remarks/>
        public void getGroupsAsync(string username, object userState) {
            if ((this.getGroupsOperationCompleted == null)) {
                this.getGroupsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetGroupsOperationCompleted);
            }
            this.InvokeAsync("getGroups", new object[] {
                        username}, this.getGroupsOperationCompleted, userState);
        }
        
        private void OngetGroupsOperationCompleted(object arg) {
            if ((this.getGroupsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getGroupsCompleted(this, new getGroupsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteFile(string name, string owner, string group, string user) {
            this.Invoke("DeleteFile", new object[] {
                        name,
                        owner,
                        group,
                        user});
        }
        
        /// <remarks/>
        public void DeleteFileAsync(string name, string owner, string group, string user) {
            this.DeleteFileAsync(name, owner, group, user, null);
        }
        
        /// <remarks/>
        public void DeleteFileAsync(string name, string owner, string group, string user, object userState) {
            if ((this.DeleteFileOperationCompleted == null)) {
                this.DeleteFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteFileOperationCompleted);
            }
            this.InvokeAsync("DeleteFile", new object[] {
                        name,
                        owner,
                        group,
                        user}, this.DeleteFileOperationCompleted, userState);
        }
        
        private void OnDeleteFileOperationCompleted(object arg) {
            if ((this.DeleteFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteFileCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/retrieve", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string retrieve(string user) {
            object[] results = this.Invoke("retrieve", new object[] {
                        user});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void retrieveAsync(string user) {
            this.retrieveAsync(user, null);
        }
        
        /// <remarks/>
        public void retrieveAsync(string user, object userState) {
            if ((this.retrieveOperationCompleted == null)) {
                this.retrieveOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveOperationCompleted);
            }
            this.InvokeAsync("retrieve", new object[] {
                        user}, this.retrieveOperationCompleted, userState);
        }
        
        private void OnretrieveOperationCompleted(object arg) {
            if ((this.retrieveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.retrieveCompleted(this, new retrieveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/uploadFiles", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool uploadFiles(long size, string group, string owner, string originalfilename, string originalfileext, string encryptedkey, string IV, [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] fileData) {
            object[] results = this.Invoke("uploadFiles", new object[] {
                        size,
                        group,
                        owner,
                        originalfilename,
                        originalfileext,
                        encryptedkey,
                        IV,
                        fileData});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void uploadFilesAsync(long size, string group, string owner, string originalfilename, string originalfileext, string encryptedkey, string IV, byte[] fileData) {
            this.uploadFilesAsync(size, group, owner, originalfilename, originalfileext, encryptedkey, IV, fileData, null);
        }
        
        /// <remarks/>
        public void uploadFilesAsync(long size, string group, string owner, string originalfilename, string originalfileext, string encryptedkey, string IV, byte[] fileData, object userState) {
            if ((this.uploadFilesOperationCompleted == null)) {
                this.uploadFilesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnuploadFilesOperationCompleted);
            }
            this.InvokeAsync("uploadFiles", new object[] {
                        size,
                        group,
                        owner,
                        originalfilename,
                        originalfileext,
                        encryptedkey,
                        IV,
                        fileData}, this.uploadFilesOperationCompleted, userState);
        }
        
        private void OnuploadFilesOperationCompleted(object arg) {
            if ((this.uploadFilesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.uploadFilesCompleted(this, new uploadFilesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/retrieveNotification", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int retrieveNotification(string username) {
            object[] results = this.Invoke("retrieveNotification", new object[] {
                        username});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void retrieveNotificationAsync(string username) {
            this.retrieveNotificationAsync(username, null);
        }
        
        /// <remarks/>
        public void retrieveNotificationAsync(string username, object userState) {
            if ((this.retrieveNotificationOperationCompleted == null)) {
                this.retrieveNotificationOperationCompleted = new System.Threading.SendOrPostCallback(this.OnretrieveNotificationOperationCompleted);
            }
            this.InvokeAsync("retrieveNotification", new object[] {
                        username}, this.retrieveNotificationOperationCompleted, userState);
        }
        
        private void OnretrieveNotificationOperationCompleted(object arg) {
            if ((this.retrieveNotificationCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.retrieveNotificationCompleted(this, new retrieveNotificationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getIsDownloaded", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool getIsDownloaded(string filename, string username, string group) {
            object[] results = this.Invoke("getIsDownloaded", new object[] {
                        filename,
                        username,
                        group});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void getIsDownloadedAsync(string filename, string username, string group) {
            this.getIsDownloadedAsync(filename, username, group, null);
        }
        
        /// <remarks/>
        public void getIsDownloadedAsync(string filename, string username, string group, object userState) {
            if ((this.getIsDownloadedOperationCompleted == null)) {
                this.getIsDownloadedOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetIsDownloadedOperationCompleted);
            }
            this.InvokeAsync("getIsDownloaded", new object[] {
                        filename,
                        username,
                        group}, this.getIsDownloadedOperationCompleted, userState);
        }
        
        private void OngetIsDownloadedOperationCompleted(object arg) {
            if ((this.getIsDownloadedCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getIsDownloadedCompleted(this, new getIsDownloadedCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getPubkeyCompletedEventHandler(object sender, getPubkeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getPubkeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getPubkeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void DownloadCompletedEventHandler(object sender, DownloadCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DownloadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DownloadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getLogsCompletedEventHandler(object sender, getLogsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getLogsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getLogsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getGroupsCompletedEventHandler(object sender, getGroupsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getGroupsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getGroupsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void DeleteFileCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void retrieveCompletedEventHandler(object sender, retrieveCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class retrieveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal retrieveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void uploadFilesCompletedEventHandler(object sender, uploadFilesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class uploadFilesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal uploadFilesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void retrieveNotificationCompletedEventHandler(object sender, retrieveNotificationCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class retrieveNotificationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal retrieveNotificationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getIsDownloadedCompletedEventHandler(object sender, getIsDownloadedCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getIsDownloadedCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getIsDownloadedCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
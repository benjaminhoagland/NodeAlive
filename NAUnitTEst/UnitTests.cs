using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;


namespace NAUnitTests
{
	[TestClass]
	public class NAUnitTests
	{
		[TestMethod] public void Test_Instance_SetActiveNode()
		{
			string guid = "";
			try
			{
				Instance.SetActiveNode(guid);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message,Instance.SetActiveNodeNullReferenceMessageInput);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Instance_SetActiveLocation()
		{
			string guid = "";
			try
			{
				Instance.SetActiveLocation(guid);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message,Instance.SetActiveLocationNullReferenceMessageInput);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Instance_AdminLock()
		{
			Instance.AdminLock();
			Assert.IsTrue(Instance.AdminLocked,"Admin lock unsuccessful.");
		}
		[TestMethod] public void Test_Instance_AdminUnlock()
		{
			Instance.AdminUnlock();
			Assert.IsFalse(Instance.AdminLocked,"Admin unlock unsuccessful.");
		}
		[TestMethod] public void Test_Instance_Message()
		{
			var item = ("New message.", 1f);
			Instance.Message(item.Item1,item.Item2);
			Assert.IsTrue(Instance.MessageQueue.Contains(item),"Failure to insert item into Instance.Message(item).");
		}
		[TestMethod] public void Test_UpdateRemoveLocationChildReference_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Update.RemoveLocationChildReference(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");

		}
		[TestMethod] public void Test_UpdateLocation_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Update.Location(input, new System.Collections.Generic.List<Data.Data.RecordStructure.Attribute>());
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");

		}
		[TestMethod] public void Test_UpdateNodeOverdue_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Update.NodeOverdue(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");

		}
		[TestMethod] public void Test_Data_Delete_Node_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Delete.Node(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Data_Delete_Location_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Delete.Location(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Data_Delete_Dispatch_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Delete.Dispatch(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Data_Delete_Script_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Delete.Script(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Data_Delete_Entity_EmptyInput()
		{
			string input = "";
			try
			{
				Data.Data.Delete.Entity(input);
			}
			catch(NullReferenceException nre)
			{
				StringAssert.Contains(nre.Message, Data.Data.GUIDnullref);
				return;
			}
			Assert.Fail("Expected NullReferenceException was not thrown.");
		}
		[TestMethod] public void Test_Data_Delete_Entity_InvalidInputGUID()
		{
			string input = "is not a guid";
			try
			{
				Data.Data.Delete.Entity(input);
			}
			catch(FormatException formatException)
			{
				StringAssert.Contains(formatException.Message, Data.Data.GUIDInputParseErrorMessage);
				return;
			}
			Assert.Fail("Expected FormatException was not thrown when parsing a non-GUID string.");
		}
		[TestMethod] public void Test_Data_Delete_Dispatch_InvalidInputGUID()
		{
			string input = "is not a guid";
			try
			{
				Data.Data.Delete.Dispatch(input);
			}
			catch(FormatException formatException)
			{
				StringAssert.Contains(formatException.Message, Data.Data.GUIDInputParseErrorMessage);
				return;
			}
			Assert.Fail("Expected FormatException was not thrown when parsing a non-GUID string.");
		}
		[TestMethod] public void Test_Data_Delete_Node_InvalidInputGUID()
		{
			string input = "is not a guid";
			try
			{
				Data.Data.Delete.Node(input);
			}
			catch(FormatException formatException)
			{
				StringAssert.Contains(formatException.Message, Data.Data.GUIDInputParseErrorMessage);
				return;
			}
			Assert.Fail("Expected FormatException was not thrown when parsing a non-GUID string.");
		}
		[TestMethod] public void Test_Data_Delete_Location_InvalidInputGUID()
		{
			string input = "is not a guid";
			try
			{
				Data.Data.Delete.Location(input);
			}
			catch(FormatException formatException)
			{
				StringAssert.Contains(formatException.Message, Data.Data.GUIDInputParseErrorMessage);
				return;
			}
			Assert.Fail("Expected FormatException was not thrown when parsing a non-GUID string.");
		}
		[TestMethod] public void Test_Data_Delete_Script_InvalidInputGUID()
		{
			string input = "is not a guid";
			try
			{
				Data.Data.Delete.Script(input);
			}
			catch(FormatException formatException)
			{
				StringAssert.Contains(formatException.Message, Data.Data.GUIDInputParseErrorMessage);
				return;
			}
			Assert.Fail("Expected FormatException was not thrown when parsing a non-GUID string.");
		}
		[TestMethod] public void Test_Log_Write_MessageEmpty()
		{
			string input = "";
			try
			{
				Log.Write(input);
			}
			catch(ArgumentException formatException)
			{
				StringAssert.Contains(formatException.Message, Log.NullOrEmptyMessageException);
				return;
			}
			Assert.Fail("Expected ArgumentException was not thrown when parsing an empty string.");
		}
		[TestMethod] public void Test_Log_Write_MessageNull()
		{
			string input = null;
			try
			{
				Log.Write(input);
			}
			catch(ArgumentException formatException)
			{
				StringAssert.Contains(formatException.Message, Log.NullOrEmptyMessageException);
				return;
			}
			Assert.Fail("Expected ArgumentException was not thrown when parsing a null valued string.");
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelBusters.NativePlugins;
using VoxelBusters.Utility;


public class ShareManager : MonoBehaviour {

	public void ShareStats()
	{
		//#if NATIVE_PLUGINS_LITE_VERSION && USES_SHARING
		ShareSheet _shareSheet = new ShareSheet();

		_shareSheet.AttachScreenShot();

		NPBinding.UI.SetPopoverPointAtLastTouchPosition();
		NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
		//#endif
	}

	//#if NATIVE_PLUGINS_LITE_VERSION && USES_SHARING
	void FinishedSharing(eShareResult _result)
	{
	Debug.Log("Finished sharing");
	Debug.Log("Share Result = " + _result);
	}
	//#endif
}



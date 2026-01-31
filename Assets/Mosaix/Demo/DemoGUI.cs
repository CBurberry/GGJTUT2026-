using UnityEngine;

public class DemoGUI: MonoBehaviour 
{
    public bool ShowSphere = true;
    public float MosaicBlocks = 20;
    bool Open = false;

    // This is true in the simple demo where we show a scale control, and false in the character
    // demo where scaling interpolates between the two control endpoints.
    public bool ScaleControl;

    public Mosaix MosaixComponent;

    GUIStyle MakePaddedBoxStyle(int Horiz, int Vert)
    {
        GUIStyle PaddedBox = new GUIStyle(GUI.skin.box); 
        PaddedBox.padding = new RectOffset(Horiz,Horiz,Vert,Vert);
        return PaddedBox;
    }
    
    void OnEnable()
    {
        Refresh();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.Space(10); // top padding
            GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                if(GUILayout.Button("Open on GitHub"))
                    Application.OpenURL("https://github.com/noisefloordev/mosaix");
            GUILayout.Space(10); // right padding
            GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(10, 10, Screen.width-20, Screen.height-20));
            GUILayout.BeginVertical(MakePaddedBoxStyle(10,10), GUILayout.MinWidth(Open? 250:10));
            Open = GUILayout.Toggle(Open, "Mosaic Controls");
            if(Open)
                ControlWindow();
            GUILayout.EndVertical();

            // Hack: Tell MayaCamera where our main UI box is, so clicking inside it doesn't move the
            // camera.  There's no GUILayout API to do this.
            MayaCamera mayaCamera = gameObject.GetComponent<MayaCamera>();
            if(Event.current.type == EventType.Repaint && mayaCamera != null)
            {
                // Get the rect for the vertical area we just drew.
                Rect LatestUIRect = GUILayoutUtility.GetLastRect();

                // GetLastRect doesn't take the area position into account, so we have to add the
                // origin given to BeginArea.  This is not a good API.
                LatestUIRect.x += 10;
                LatestUIRect.y += 10;

                // Different coordinate spaces:
                LatestUIRect.y = Screen.height - LatestUIRect.x - LatestUIRect.height;

                mayaCamera.IgnoreRect = LatestUIRect;
            }

        GUILayout.EndArea();

        Refresh();
    }

    void ControlWindow()
    {
        MosaixComponent.enabled = GUILayout.Toggle(MosaixComponent.enabled, "Enable mosaic");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Blocks");
        MosaicBlocks = GUILayout.HorizontalSlider(MosaicBlocks, 2, 100);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Alpha");
        MosaixComponent.Alpha = GUILayout.HorizontalSlider(MosaixComponent.Alpha, 0, 1);
        GUILayout.EndHorizontal();
        MosaixComponent.SphereMasking = GUILayout.Toggle(MosaixComponent.SphereMasking, "Sphere masking");

        if(MosaixComponent.MaskingTexture != null)
            MosaixComponent.TextureMasking = GUILayout.Toggle(MosaixComponent.TextureMasking, "Texture masking");
        MosaixComponent.ShowMask = GUILayout.Toggle(MosaixComponent.ShowMask, "Show mask");
    }

    int level = 0;
    void BeginGroup()
    {
        ++level;
        GUILayout.BeginHorizontal();
        GUILayout.Space(level*15); // left padding
        GUILayout.BeginVertical();
    }

    void EndGroup()
    {
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
        --level;
    }

    void Refresh()
    {
        MosaixComponent.MosaicBlocks = MosaicBlocks;
    }
}

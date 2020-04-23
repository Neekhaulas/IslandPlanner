using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

public class World : MonoBehaviour
{
    public int[,,] Map = new int[225, 240, 4]; 
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 15 * 5; i++)
        {
            for (int j = 0; j < 15 * 6; j++)
            {
                if (i < 10 || i > 15 * 5 - 3 || j < 2 || j > 15 * 6 - 3) Map[j, i, 0] = 0;
                else Map[j, i, 0] = 1;
            }
        }
        Debug.Log(ObjectToString(Map));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public static string ObjectToString(int[,,] ar)
    {
        using (MemoryStream ms = new MemoryStream())
        {

            StreamWriter writer = new StreamWriter(ms);
            for (int y = 0; y < 4; y++)
            {
                for (int i = 0; i < 15 * 5; i++)
                {
                    for (int j = 0; j < 15 * 6; j++)
                    {
                        writer.Write(ar[j, i, y]);
                    }
                }
            }
            writer.Close();
            return System.Convert.ToBase64String(Zip(System.Convert.ToBase64String(ms.ToArray())));
        }
    }

    public static void CopyTo(Stream src, Stream dest)
    {
        byte[] bytes = new byte[4096];

        int cnt;

        while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
        {
            dest.Write(bytes, 0, cnt);
        }
    }

    public static byte[] Zip(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);

        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                //msi.CopyTo(gs);
                CopyTo(msi, gs);
            }

            return mso.ToArray();
        }
    }

    public static string Unzip(byte[] bytes)
    {
        using (var msi = new MemoryStream(bytes))
        using (var mso = new MemoryStream())
        {
            using (var gs = new GZipStream(msi, CompressionMode.Decompress))
            {
                //gs.CopyTo(mso);
                CopyTo(gs, mso);
            }

            return Encoding.UTF8.GetString(mso.ToArray());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Random = System.Random;
using System;
//Maayan&Shira
using ViveSR.anipal.Eye;


public class camera_pos : MonoBehaviour
{
    //members
    public StreamWriter writer;
    Random rnd = new Random();
    int i = 0;
    bool shuffled = false;
    bool is_prac = true;
    DateTime time;
    double first_time;
    double last_time;
    double current_time;
    List<List<Vector3>> all_features;
    List<List<Vector3>> all_features_prac;
    List<int> idx_rnd;
    List<string> idx_real;
    List<string> classification;
    int[] rnd_idx;
    GameObject l;
    GameObject u;
    GameObject p_u;
    GameObject p_l;
    GameObject cam;
    GameObject cam1;
    TextMesh txt;
    int b_r;
    // Start is called before the first frame update

    //Maayan&Shira
    // Create a StreamWriter instance to write to the CSV file
    StreamWriter writer2;
    VerboseData d = new VerboseData();

    void Start()
    {
        //initializing all members
        time = DateTime.Now;
        first_time = time.TimeOfDay.TotalMilliseconds;
        writer = new StreamWriter("participants/" + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".csv");
        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5}", "f_name", "l_name", "Age", "Gender", "Handedness", "Correction"));
        writer.WriteLine();
        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "r/b_Answer", "R/B_Circle", "RT", "trial_start", "Class", "Index", "Distance_Delta", "Answer"));//columns titles
        writer.Flush();
        l = GameObject.Find("point_1_L");
        u = GameObject.Find("point_1_U");
        p_l = GameObject.Find("p_l");   
        p_u = GameObject.Find("p_u");
        txt = GameObject.Find("inst").GetComponent<TextMesh>();
        cam = GameObject.Find("[CameraRig]");
        cam1 = GameObject.Find("Camera");
        l.GetComponent<MeshRenderer>().enabled = false; //make the cubes unvisable
        u.GetComponent<MeshRenderer>().enabled = false;
        all_features = new List<List<Vector3>>();
        all_features_prac = new List<List<Vector3>>();
        idx_rnd = new List<int>();
        idx_real = new List<string>();
        classification = new List<string>();
        
        string path = @"exp_3d_info.csv";
        read(path); //init all_featurs with the exp data stored in the path
        rnd_idx = new int[all_features.Count];
        Debug.Log(all_features_prac.Count);
        all_features_prac.Add(new List<Vector3>());
        Debug.Log(all_features_prac.Count);
        all_features_prac[all_features_prac.Count - 1].Add(new Vector3(2573.79004f, -446.209991f, 239.449997f));
        
        for (int j = 0; j < all_features_prac[0].Count-1; j++)
        {
            all_features_prac[all_features_prac.Count - 1].Add(new Vector3(0f, 0f, 0f));
        }
        Debug.Log(all_features_prac[all_features_prac.Count - 1].Count);

        //Maayan&Shira
        string filePath = "C:/Users/shagidolab/Desktop/Maayan and Shira/check.csv";

        // Open the CSV file for writing (or create a new one if it doesn't exist)
        writer2 = new StreamWriter(filePath, false);

        // Write the header row to the CSV file
        writer2.WriteLine("Frame,Left Eye openness,Right Eye Openness,Left Pupil Position,Right Pupil Position,Left Pupil Diameter,Right Pupil Diameter");
    }

    // Update is called once per frame
    void Update()
    {
        //Maayan&Shira
        SRanipal_Eye.GetVerboseData(out d);
        writer2.WriteLine(Time.frameCount + "," + d.left.eye_openness + "," + d.right.eye_openness + "," + d.left.pupil_position_in_sensor_area + "," + d.right.pupil_position_in_sensor_area + "," + d.left.pupil_diameter_mm + "," + d.right.pupil_diameter_mm);


        if (!shuffled)
        {
            shuffle();
            last_time = time.TimeOfDay.TotalMilliseconds;
        }
        
        if (Input.GetKeyDown(KeyCode.I)) //geting the index of the current trial when pressing i
        {
            if (i > 0)
            {
                Debug.Log(idx_rnd[i - 1]);
            }
        }
        //
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || (Input.GetMouseButtonDown(2) && is_prac))
        {
            //updating the RT
            time = DateTime.Now;
            current_time = time.TimeOfDay.TotalMilliseconds;
            if(i < all_features.Count)
            {
                if (i == all_features_prac.Count - 1)
                {
                    txt.text = "Press the middle mouse button to begin the experiment";
                }
                if(i >= all_features_prac.Count && is_prac && Input.GetMouseButtonDown(2))
                {
                    i = 0;
                    is_prac = false;
                }
                Debug.Log(i); //triggered every time the user clicks the mouse 

                if (is_prac && i < all_features_prac.Count)
                {
                    last_time = current_time;
                    cam.transform.position = all_features_prac[i][0]; //updating the camera position
                    cam.transform.rotation = Quaternion.identity;
                    cam.transform.Rotate(all_features_prac[i][1], Space.Self);//updating the camera rotation
                    l.transform.position = all_features_prac[i][2];//updating the lower point position
                    u.transform.position = all_features_prac[i][3];//updating the upper point position
                    l.transform.localScale = all_features_prac[i][4];//updating the lower point scale
                    u.transform.localScale = all_features_prac[i][4];//updating the upper point scale
                    p_l.transform.position = all_features_prac[i][5];//updating the lower circle position
                    p_u.transform.position = all_features_prac[i][7];//updating the upper circle position
                    p_l.transform.LookAt(cam.transform);
                    p_u.transform.LookAt(cam.transform);
                    p_l.transform.localScale = all_features_prac[i][6];//updating the lower circle scale
                    p_u.transform.localScale = all_features_prac[i][8];//updating the upper circle scale
                    if (rnd.Next(2) == 0)//changing the circles colors randomly
                    {
                        p_l.GetComponent<TextMesh>().color = Color.red;
                        p_u.GetComponent<TextMesh>().color = Color.blue;
                    }
                    else
                    {
                        p_l.GetComponent<TextMesh>().color = Color.blue;
                        p_u.GetComponent<TextMesh>().color = Color.red;
                    }
                }
                if (!is_prac)
                {
                    string ans;
                    //prepering the data of each trial for writing to the csv file
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (b_r == 0)
                            ans = "L";
                        else
                            ans = "U";
                        string delta = ((Vector3.Distance(l.transform.position, cam1.transform.position) - Vector3.Distance(u.transform.position, cam1.transform.position)).ToString());
                        Debug.Log(classification[idx_rnd[i]]);
                        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "r", b_r.ToString(), (current_time - last_time).ToString(), (last_time - first_time).ToString(), classification[idx_rnd[i - 1]], idx_real[idx_rnd[i - 1]], delta, ans));
                        writer.Flush();
                        //Maayan&Shira
                        writer2.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "r", b_r.ToString(), (current_time - last_time).ToString(), (last_time - first_time).ToString(), classification[idx_rnd[i - 1]], idx_real[idx_rnd[i - 1]], delta, ans));
                    }
                    if (Input.GetMouseButtonDown(1))
                    {
                        if (b_r == 0)
                            ans = "U";
                        else
                            ans = "L";
                        string delta = ((Vector3.Distance(l.transform.position, cam1.transform.position) - Vector3.Distance(u.transform.position, cam1.transform.position)).ToString());
                        writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "b", b_r.ToString(), (current_time - last_time).ToString(), (last_time - first_time).ToString(), classification[idx_rnd[i - 1]], idx_real[idx_rnd[i - 1]], delta, ans));
                        writer.Flush();
                        //Maayan&Shira
                        writer2.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", "b", b_r.ToString(), (current_time - last_time).ToString(), (last_time - first_time).ToString(), classification[idx_rnd[i - 1]], idx_real[idx_rnd[i - 1]], delta, ans));
                    }
                    b_r = rnd.Next(2);

                    
                    last_time = current_time;
                    cam.transform.position = all_features[idx_rnd[i]][0]; //updating the camera position
                    cam.transform.rotation = Quaternion.identity;
                    cam.transform.Rotate(all_features[idx_rnd[i]][1], Space.Self);//updating the camera rotation
                    l.transform.position = all_features[idx_rnd[i]][2];//updating the lower point position
                    u.transform.position = all_features[idx_rnd[i]][3];//updating the upper point position
                    l.transform.localScale = all_features[idx_rnd[i]][4];//updating the lower point scale
                    u.transform.localScale = all_features[idx_rnd[i]][4];//updating the upper point scale
                    p_l.transform.position = all_features[idx_rnd[i]][5];//updating the lower circle position
                    p_u.transform.position = all_features[idx_rnd[i]][7];//updating the upper circle position
                    p_l.transform.LookAt(cam.transform);
                    p_u.transform.LookAt(cam.transform);
                    p_l.transform.localScale = all_features[idx_rnd[i]][6];//updating the lower circle scale
                    p_u.transform.localScale = all_features[idx_rnd[i]][8];//updating the upper circle scale
                    if (b_r == 0) //changing the circles colors randomly
                    {
                        p_l.GetComponent<TextMesh>().color = Color.red;
                        p_u.GetComponent<TextMesh>().color = Color.blue;
                    }
                    else
                    {
                        p_l.GetComponent<TextMesh>().color = Color.blue;
                        p_u.GetComponent<TextMesh>().color = Color.red;
                    }
                    //Maayan&Shira
                    writer2.WriteLine("----------------------------------NEW SCENE----------------------------------");

                }

                i++;
            }
            else
            {
                cam.transform.position = new Vector3(2573.79004f, -446.209991f, 239.449997f);
                cam.transform.rotation = Quaternion.identity;
                cam.transform.Rotate(new Vector3(0f, 0f, 0f), Space.Self);
                txt.text = "The experiment ended," + '\n' + "Thank you for your participation!";
            }
        }
    }
    //This function init a streamReader object.
    //In the exp_info file each row contains 9 cells that contains 3 numerical vals seperated by ';'.
    //in every iteration of the loop a new row of the csv file is read
    //and for every cell a new vector3 object is made and added to the right location in all_features list.
    //in the end of this function the all_features list contains the all vector3s that describe all the trials.
    void read(string path)
    {
        Debug.Log(path);
        using (var reader = new StreamReader(path))
        {
            Debug.Log("file opend");
            int idx = 0;
            List<float> rep_vec = new List<float>();
            bool for_prac = true;
            while (!reader.EndOfStream)
            {

                var line = reader.ReadLine();
                var values = line.Split(',');
                if(for_prac)
                    all_features_prac.Add(new List<Vector3>());
                else
                {
                    all_features.Add(new List<Vector3>());
                    classification.Add(values[values.Length - 2]);
                    idx_real.Add(values[values.Length - 1]);
                }
              
                for (int i = 0; i < values.Length - 2; i++)
                {
                    values[i].Remove(0);
                    var vec3 = values[i].Split(';');
                    for (int j = 0; j < vec3.Length; j++)
                    {
                        if (vec3[j][0] == '\'')
                        {
                            vec3[j].Remove(0);
                        }
                        rep_vec.Add(float.Parse(vec3[j]));
                    }
                    if(i == 0)
                    {
                        if (for_prac)
                            all_features_prac[idx].Add(new Vector3(rep_vec[0], rep_vec[1] - 1.25f, rep_vec[2]));
                        else
                            all_features[idx].Add(new Vector3(rep_vec[0], rep_vec[1] - 1.25f, rep_vec[2]));
                    }
                    else
                    {
                        if (for_prac)
                            all_features_prac[idx].Add(new Vector3(rep_vec[0], rep_vec[1], rep_vec[2]));
                        else
                            all_features[idx].Add(new Vector3(rep_vec[0], rep_vec[1], rep_vec[2]));
                    }
                    rep_vec.Clear();
                }
                idx += 1;
                if(idx > 2 && for_prac)
                {
                    idx = 0;
                    for_prac = false;
                }
            }
            reader.Close();
            
        }
    }
    void write(string path, List<List<Vector3>> all_features)
    {
        using (var w = new StreamWriter(path))
        {
            for (int i = 0; i < all_features.Count; i++)
            {
                Debug.Log(i);
                var line = string.Format("{0};{1};{2},{3};{4};{5},{6};{7};{8},{9};{10};{11},{12};{13};{14},{15};{16};{17},{18};{19};{20},{21};{22};{23},{24};{25};{26}",
                    all_features[i][0][0], all_features[i][0][1], all_features[i][0][2],
                    all_features[i][1][0], all_features[i][1][1], all_features[i][1][2],
                    all_features[i][2][0], all_features[i][2][1], all_features[i][2][2],
                    all_features[i][3][0], all_features[i][3][1], all_features[i][3][2],
                    all_features[i][4][0], all_features[i][4][1], all_features[i][4][2],
                    all_features[i][5][0], all_features[i][5][1], all_features[i][5][2],
                    all_features[i][6][0], all_features[i][6][1], all_features[i][6][2],
                    all_features[i][7][0], all_features[i][7][1], all_features[i][7][2],
                    all_features[i][8][0], all_features[i][8][1], all_features[i][8][2]);
                Debug.Log(line);
                w.WriteLine(line);
                w.Flush();
            }
        }

    }

    void shuffle()
    {
        shuffled = true;
        Debug.Log("shuffle started");
        
        for (int i = 0; i < all_features.Count; i++)
        {
            rnd_idx[i] = -1;
        }
        int local_rnd;
        for(int i = 0; i < all_features.Count; i++)
        {
            local_rnd = rnd.Next(all_features.Count);
            while (rnd_idx[local_rnd] != -1)
            {
                local_rnd = rnd.Next(all_features.Count);
            }
            rnd_idx[local_rnd] = i;
            idx_rnd.Add(local_rnd);
        }
        
    }
    void OnApplicationQuit()
    {
        writer.Close();
        //Maayan&Shira
        writer2.Close();
    }
}

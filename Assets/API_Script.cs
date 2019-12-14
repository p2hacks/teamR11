using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MiniJSON;

public class API_Script : MonoBehaviour
{
    private const string url = "http://api.openweathermap.org/data/2.5/weather?q="; //リクエスト送信先
    private const string locale = "Hakodate,jp"; // "(city name),(country code)"で指定
    private string KEY = null; //外部ファイルから読み込んだAPIキーを格納する変数
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
     //APIキーの取得
        try{
            //もしWindowsで作業フォルダのパスが取れてない場合は下のメソッドをパスの前に足す
            //   Directory.GetCurrentDirectory()
            //そのうえでスラッシュ(/)をバックスラッシュ(¥)にする
            using (StreamReader sr = new StreamReader("Assets/API_KEY.txt")){
                KEY = sr.ReadToEnd();
            }
        }
        catch (Exception e){
            Debug.Log("The file could not be read:");
            Debug.Log(e.Message);
        }
     //APIリクエスト送信
        WWW www = new WWW(url + locale + "&APPID=" + KEY);
        yield return www;
        //JSONから値を取得
        var jsonDict = Json.Deserialize(www.text) as Dictionary<string, object>;
        

     //気温
        var main = (IDictionary)jsonDict["main"];
        double Kelvin = (double)main["temp"]; //ケルビン(K)
        int Celsius = (int)Math.Round(Kelvin - 273.15); //摂氏(°C)に変換
        Debug.Log("Celsius = " + Celsius + "°C");
        //Celsiusによる雪だるまの描画を分類
        Boolean snowman_melt;
        if (Celsius > 3)
        {
            snowman_melt = true; //雪だるまが溶ける
        }
        else
        {
            snowman_melt = false;
        }


     //天気
        var weather_arr = (IList)jsonDict["weather"];
        var weather_obj = (IDictionary)weather_arr[0];
        long weather_id = (long)weather_obj["id"]; //天気のid
        //weather_idによる天気の描画を分類
        int weather_class;
        switch (weather_id/100){
            case 3: //霧雨クラス
                weather_class = 1; //小雨
                break;
            case 5: //雨クラス
                if(weather_id >= 502) weather_class = 2; //大雨
                else weather_class = 1; //小雨
                break;
            case 6: //雪クラス
                if(weather_id <= 601 || weather_id == 611) {
                    weather_class = 3; //雪
                }
                else if(weather_id == 615 || weather_id == 616) {
                    weather_class = 5; //雨と雪
                }
                else weather_class = 4; //大雪
                break;
            default:
                weather_class = 0; //割当なし
                break;
        }

     //デバッグ用アウトプット群
        var weather = weather_obj["main"];
        Debug.Log("Weather : " + weather);
        Debug.Log("weather_id : " + weather_id);
        Debug.Log("weather_class : " + weather_class);
        Debug.Log("Celsius : " + Celsius);
        Debug.Log("snowman_melt : " + snowman_melt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

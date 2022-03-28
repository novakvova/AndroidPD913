package com.example.newmail;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;

import com.android.volley.toolbox.NetworkImageView;
import com.example.newmail.constants.Urls;
import com.example.newmail.network.ImageRequester;

public class MainActivity extends AppCompatActivity {

    private ImageRequester imageRequester;
    private NetworkImageView myImage;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        imageRequester = ImageRequester.getInstance();
        myImage = findViewById(R.id.myimg);
        String urlImg = Urls.BASE+"/images/1.jpg";
        imageRequester.setImageFromUrl(myImage, urlImg);
    }
}
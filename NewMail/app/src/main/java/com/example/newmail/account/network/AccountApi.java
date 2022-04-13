package com.example.newmail.account.network;


import com.example.newmail.account.dto.AccountResponseDTO;
import com.example.newmail.account.dto.LoginDTO;
import com.example.newmail.account.dto.RegisterDTO;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

public interface AccountApi {
    @POST("/api/account/register")
    public Call<AccountResponseDTO> register(@Body RegisterDTO model);
    @POST("/api/account/login")
    public Call<AccountResponseDTO> login(@Body LoginDTO model);
}
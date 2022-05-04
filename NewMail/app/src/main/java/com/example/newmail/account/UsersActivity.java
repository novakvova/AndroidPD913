package com.example.newmail.account;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.GridLayoutManager;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.newmail.BaseActivity;
import com.example.newmail.R;
import com.example.newmail.account.dto.UserDTO;
import com.example.newmail.account.network.AccountService;
import com.example.newmail.account.userscard.UsersAdapter;
import com.example.newmail.application.HomeApplication;
import com.example.newmail.utils.CommonUtils;

import android.os.Bundle;
import android.widget.Toast;

import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class UsersActivity extends BaseActivity {

    private UsersAdapter adapter;
    private RecyclerView rcvUsers;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_users);

        rcvUsers = findViewById(R.id.rcvUsers);
        rcvUsers.setHasFixedSize(true);
        rcvUsers.setLayoutManager(new GridLayoutManager(this, 2,
                LinearLayoutManager.VERTICAL, false));

        CommonUtils.showLoading(this);
        AccountService.getInstance()
                .jsonApi()
                .users()
                .enqueue(new Callback<List<UserDTO>>() {
                    @Override
                    public void onResponse(Call<List<UserDTO>> call, Response<List<UserDTO>> response) {
                        if(response.isSuccessful())
                        {
                            adapter=new UsersAdapter(response.body(),
                                            UsersActivity.this::onClickByItem,
                                            UsersActivity.this::onClickEditUser);
                            rcvUsers.setAdapter(adapter);
                        }
                        CommonUtils.hideLoading();
                    }

                    @Override
                    public void onFailure(Call<List<UserDTO>> call, Throwable t) {
                        CommonUtils.hideLoading();
                    }
                });

    }

    private void onClickByItem(UserDTO user) {
        Toast.makeText(HomeApplication.getAppContext(), user.getEmail(), Toast.LENGTH_LONG).show();
    }

    private void onClickEditUser(UserDTO user) {
        Toast.makeText(HomeApplication.getAppContext(), "EditUser "+user.getEmail(), Toast.LENGTH_LONG).show();
    }
}


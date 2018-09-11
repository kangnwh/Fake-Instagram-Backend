package com.unimelb.mobile.dtos;

import com.fasterxml.jackson.annotation.JsonIgnore;
import lombok.NonNull;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;

import java.io.UnsupportedEncodingException;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import java.util.Date;

/**
 * UserCreateModel
 * <p>
 * Author Ning Kang
 * Date 11/9/18
 */


public class UserCreateDTO {

	@NonNull
	private String username;

	@NonNull
	private String password;

	@NonNull
	private String name;

	@NonNull
	private String email;

	@NonNull
	private String phone;

	@NonNull
	private Date dob;

	@NonNull
	private String gender;

	@JsonIgnore
	private Date createDate;

	public UserCreateDTO() {
		createDate= new Date();
	}

	public String getUsername() {
		return username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getPassword() {
		BCryptPasswordEncoder bCryptPasswordEncoder = new BCryptPasswordEncoder();
		return bCryptPasswordEncoder.encode(password);
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

	public Date getDob() {
		return dob;
	}

	public void setDob(Date dob) {
		this.dob = dob;
	}

	public String getGender() {
		return gender;
	}

	public void setGender(String gender) {
		this.gender = gender;
	}

	public Date getCreateDate() {
		return createDate;
	}
}

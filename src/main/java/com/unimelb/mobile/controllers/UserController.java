package com.unimelb.mobile.controllers;

import com.unimelb.mobile.dtos.UserCreateDTO;
import com.unimelb.mobile.dtos.UserModels;
import com.unimelb.mobile.orm.User;
import com.unimelb.mobile.orm.UserRepository;
import io.swagger.annotations.*;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

/**
 * UserController
 * <p>
 * Author Ning Kang
 * Date 10/9/18
 */

@RestController
@RequestMapping("/v1/users")
public class UserController {

	private final UserRepository userRepository;

	private final ModelMapper modelMapper;



	@Autowired
	public UserController(UserRepository userRepository, ModelMapper modelMapper) {
		this.userRepository = userRepository;
		this.modelMapper = modelMapper;
	}

	@RequestMapping(value = "/", method = RequestMethod.POST)
	public List<User> allUsers(){
		return userRepository.findAll();

	}

//	@ApiOperation(value = "User Login")
//	@PostMapping(name ="Login " ,value = "/login")
//	public String login(@RequestBody UserModels.LoginModel loginInfo){
//		return "login";
//	}
//
//	@GetMapping(value =  "/login")
//	public String login(){
//		return "login";
//	}


	@ApiOperation(value = "User Creation")
	@PostMapping(value = "/sign-up")
	public String newUser(@RequestBody UserCreateDTO createdUser){
		User user = modelMapper.map(createdUser, User.class);
		userRepository.save(user);
		userRepository.flush();

		return HttpStatus.CREATED.toString();

	}

	@RequestMapping(value="/{id}", method=RequestMethod.GET)
	public User getUser(@PathVariable Long id) {

		Optional<User> oUser = userRepository.findById(id);
		return oUser.orElse(null);

	}

	@RequestMapping(value="/{id}", method=RequestMethod.PUT)
	public String putUser(@PathVariable Long id, @ModelAttribute User user) {
		// 处理"/users/{id}"的PUT请求，用来更新User信息

		return "success";
	}

	@RequestMapping(value="/{id}", method=RequestMethod.DELETE)
	public String deleteUser(@PathVariable Long id) {
		// 处理"/users/{id}"的DELETE请求，用来删除User
		return "success";
	}

}

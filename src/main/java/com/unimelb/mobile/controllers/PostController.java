package com.unimelb.mobile.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import springfox.documentation.spring.web.json.Json;

import java.util.Arrays;

/**
 * HomeController
 * <p>
 * Author Ning Kang
 * Date 9/9/18
 */

@RestController
@RequestMapping("/v1/post")
public class PostController {

	@RequestMapping("/hot")
	public Json index(){
		return new Json(Arrays.toString(new String[]{"hot", "posts"}));
	}

	@RequestMapping("/followed")
	public Json followed(){
		return new Json(Arrays.toString(new String[]{"this is", "followed"}));
	}
}

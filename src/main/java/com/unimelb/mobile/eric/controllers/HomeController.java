package com.unimelb.mobile.eric.controllers;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

/**
 * HomeController
 * <p>
 * Author Ning Kang
 * Date 9/9/18
 */

@RestController
public class HomeController {

	@RequestMapping("/")
	public String index(){
		return "Hello World";
	}
}
